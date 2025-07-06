using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices;
using FitManager_Web_Services.Inventory.Application.Internal.QueryServices;
using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Domain.Model.Queries; // This using is correct
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;
using FitManager_Web_Services.Inventory.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ItemTypesController : ControllerBase
    {
        private readonly ItemTypeCommandService _itemTypeCommandService;
        private readonly ItemTypeQueryService _itemTypeQueryService;

        public ItemTypesController(ItemTypeCommandService itemTypeCommandService, ItemTypeQueryService itemTypeQueryService)
        {
            _itemTypeCommandService = itemTypeCommandService;
            _itemTypeQueryService = itemTypeQueryService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar Tipos de Ítem", Description = "Obtiene una lista de todos los tipos de ítem registrados.")]
        public async Task<ActionResult<IEnumerable<ItemTypeResource>>> GetAllItemTypes()
        {
            var getAllQuery = new GetAllItemTypesQuery(); // <-- Corrected here
            var itemTypes = await _itemTypeQueryService.Handle(getAllQuery);
            var itemTypeResources = ItemTypeResourceFromEntityAssembler.ToResourceListFromEntityList(itemTypes);
            return Ok(itemTypeResources);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear Tipo de Ítem", Description = "Crea un nuevo tipo de ítem en el sistema.")]
        public async Task<IActionResult> CreateItemType([FromBody] CreateItemTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createCommand = CreateItemTypeCommandFromResourceAssembler.ToCommandFromResource(resource);
            
            var createdItemType = await _itemTypeCommandService.Handle(createCommand); 

            if (createdItemType == null)
            {
                return BadRequest("Failed to create item type. It might already exist or there was another business rule violation.");
            }

            var itemTypeResource = ItemTypeResourceFromEntityAssembler.ToResourceFromEntity(createdItemType);
            return CreatedAtAction(nameof(GetAllItemTypes), new { id = itemTypeResource?.Id }, itemTypeResource);
        }
    }
}