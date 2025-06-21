using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices;
using FitManager_Web_Services.Inventory.Domain.Model.Commands;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers
{
    /// <summary>
    /// API controller for managing supply purchases.
    /// </summary>
    /// <remarks>
    /// Provides RESTful endpoints for registering new supply purchases, their associated details,
    /// and the creation of corresponding inventory items, as well as retrieving existing purchases.
    /// </remarks>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SupplyPurchaseController : ControllerBase
    {
        private readonly SupplyPurchaseCommandService _supplyPurchaseCommandService;
        private readonly SupplyPurchaseQueryService _supplyPurchaseQueryService;
        private readonly PurchaseDetailCommandService _purchaseDetailCommandService;
        private readonly ItemCommandService _itemCommandService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyPurchaseController"/> class.
        /// </summary>
        /// <param name="supplyPurchaseCommandService">The command service for supply purchase operations.</param>
        /// <param name="supplyPurchaseQueryService">The query service for supply purchase retrieval.</param>
        /// <param name="purchaseDetailCommandService">The command service for purchase detail operations.</param>
        /// <param name="itemCommandService">The command service for item operations.</param>
        public SupplyPurchaseController(
            SupplyPurchaseCommandService supplyPurchaseCommandService,
            SupplyPurchaseQueryService supplyPurchaseQueryService,
            PurchaseDetailCommandService purchaseDetailCommandService,
            ItemCommandService itemCommandService)
        {
            _supplyPurchaseCommandService = supplyPurchaseCommandService;
            _supplyPurchaseQueryService = supplyPurchaseQueryService;
            _purchaseDetailCommandService = purchaseDetailCommandService;
            _itemCommandService = itemCommandService;
        }

        /// <summary>
        /// Registers a new supply purchase, its details (PurchaseDetails), and creates the associated inventory items.
        /// </summary>
        /// <param name="resource">The resource containing details for the new supply purchase, including nested purchase details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the registration operation.
        /// Returns 201 Created with the created supply purchase resource on success,
        /// or 400 BadRequest if validation fails or the purchase cannot be registered.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register Supply Purchase",
            Description = "Registers a new supply purchase, its details (PurchaseDetails), and creates the associated inventory items."
        )]
        [SwaggerResponse(201, "The supply purchase was registered successfully.", typeof(SupplyPurchaseResource))]
        [SwaggerResponse(400, "Invalid input data or an internal error prevented the purchase registration.")]
        public async Task<IActionResult> Create([FromBody] CreateSupplyPurchaseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = SupplyPurchaseFromResourceAssembler.ToEntityFromResource(resource); 
            var supplyPurchase = await _supplyPurchaseCommandService.CreateAsync( 
                entity.Date,
                entity.Amount,
                entity.Method,
                entity.Currency,
                entity.VendorName
            );

            if (supplyPurchase == null)
                return BadRequest("No se pudo registrar la compra de suministros debido a un error interno.");

            if (resource.PurchaseDetails != null && resource.PurchaseDetails.Any())
            {
                foreach (var detailResource in resource.PurchaseDetails)
                {
                    var purchaseDetail = await _purchaseDetailCommandService.CreateAsync( 
                        supplyPurchase.Id,          
                        detailResource.ItemTypeId,
                        detailResource.UnitPrice,
                        detailResource.Quantity
                    );

                    if (purchaseDetail == null)
                    {
                        Console.WriteLine($"Advertencia: No se pudo crear el detalle de compra para ItemType {detailResource.ItemTypeId}. La compra continúa, pero este detalle y sus ítems no se registrarán.");
                        continue; 
                    }

                    for (int i = 0; i < detailResource.Quantity; i++)
                    {
                        var createItemCommand = new CreateItemCommand(
                            detailResource.LastMaintenanceDate,
                            detailResource.NextMaintenanceDate,
                            detailResource.Status,            
                            detailResource.EmployeeId,        
                            detailResource.ItemTypeId 
                        );
                        
                        var createdItem = await _itemCommandService.Handle(createItemCommand);

                        if (createdItem == null)
                        {
                            Console.WriteLine($"Advertencia: No se pudo crear el ítem (unidad {i+1}) de tipo {detailResource.ItemTypeId} " +
                                              $"para la compra {supplyPurchase.Id}.");
                        }
                    }
                }
            }

            var supplyPurchaseResource = SupplyPurchaseToResourceAssembler.ToResourceFromEntity(supplyPurchase);
            return Created($"api/v1/supplypurchase/{supplyPurchase.Id}", supplyPurchaseResource);
        }
        
        /// <summary>
        /// Retrieves all supply purchases registered in the system.
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing an enumerable of <see cref="SupplyPurchaseResource"/> objects.
        /// Returns 200 OK with the list of supply purchases.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "List Supply Purchases",
            Description = "Retrieves all supply purchases registered in the system."
        )]
        [SwaggerResponse(200, "A list of supply purchases was retrieved successfully.", typeof(IEnumerable<SupplyPurchaseResource>))]
        public async Task<ActionResult<IEnumerable<SupplyPurchaseResource>>> GetAll()
        {
            var purchases = await _supplyPurchaseQueryService.GetAllAsync();
            var resources = SupplyPurchaseToResourceAssembler.ToResourceListFromEntityList(purchases);
            return Ok(resources);
        }
    }
}