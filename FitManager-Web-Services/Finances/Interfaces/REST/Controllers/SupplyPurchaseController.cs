using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System; // Necesario para DateTime y Console.WriteLine

// Usings existentes del contexto de Finances
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;
using FitManager_Web_Services.Finances.Domain.Model.Commands; // Para CreateSupplyPurchaseCommand, CreatePurchaseDetailCommand

// Usings del contexto de Inventory (ya los tenías, solo confirmando su necesidad)
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices; // Para ItemCommandService
using FitManager_Web_Services.Inventory.Interfaces.REST.Transform; // Para CreateItemCommandFromResourceAssembler
using FitManager_Web_Services.Inventory.Domain.Model.Commands; // Para CreateItemCommand

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SupplyPurchaseController : ControllerBase
    {
        private readonly SupplyPurchaseCommandService _supplyPurchaseCommandService;
        private readonly SupplyPurchaseQueryService _supplyPurchaseQueryService;
        private readonly PurchaseDetailCommandService _purchaseDetailCommandService; // <-- ¡NUEVO: Declaración del servicio de PurchaseDetail!
        private readonly ItemCommandService _itemCommandService; // Ya la tenías

        public SupplyPurchaseController(
            SupplyPurchaseCommandService supplyPurchaseCommandService,
            SupplyPurchaseQueryService supplyPurchaseQueryService,
            PurchaseDetailCommandService purchaseDetailCommandService, // <-- ¡INJECTAR el servicio de PurchaseDetail!
            ItemCommandService itemCommandService)
        {
            _supplyPurchaseCommandService = supplyPurchaseCommandService;
            _supplyPurchaseQueryService = supplyPurchaseQueryService;
            _purchaseDetailCommandService = purchaseDetailCommandService; // <-- ¡ASIGNAR!
            _itemCommandService = itemCommandService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Registrar una compra de insumos",
            Description = "Registra una nueva compra de suministros, sus detalles (PurchaseDetails) y crea los ítems de inventario asociados."
        )]
        public async Task<IActionResult> Create([FromBody] CreateSupplyPurchaseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1. Crear la SupplyPurchase principal
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

            // 2. Procesar los PurchaseDetails y crear Items
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

                    // 3. Por cada 'Quantity' en el PurchaseDetail, crear una instancia individual de Item
                    for (int i = 0; i < detailResource.Quantity; i++)
                    {
                        // --- ¡MODIFICADO: USAR DATOS DEL detailResource! ---
                        var createItemCommand = new CreateItemCommand(
                            detailResource.LastMaintenanceDate, // <-- ¡Ahora viene del JSON!
                            detailResource.NextMaintenanceDate, // <-- ¡Ahora viene del JSON!
                            detailResource.Status,              // <-- ¡Ahora viene del JSON!
                            detailResource.EmployeeId,          // <-- ¡Ahora viene del JSON!
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
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar compras de insumos",
            Description = "Obtiene todas las compras de insumos registradas en el sistema."
        )]
        public async Task<ActionResult<IEnumerable<SupplyPurchaseResource>>> GetAll()
        {
            var purchases = await _supplyPurchaseQueryService.GetAllAsync();
            var resources = SupplyPurchaseToResourceAssembler.ToResourceListFromEntityList(purchases);
            return Ok(resources);
        }
    }
}