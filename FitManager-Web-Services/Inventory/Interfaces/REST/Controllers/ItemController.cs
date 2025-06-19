using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
// Asegúrate de que los using son correctos para tu proyecto
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices;
using FitManager_Web_Services.Inventory.Application.Internal.QueryServices;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;
using FitManager_Web_Services.Inventory.Interfaces.REST.Transform;
using FitManager_Web_Services.Inventory.Domain.Model.Commands; // Mantener si DeleteItemCommand se usa
using FitManager_Web_Services.Inventory.Domain.Model.Queries;   // Mantener si GetAllItemsQuery se usa
using Swashbuckle.AspNetCore.Annotations;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Controllers
{
    /// <summary>
    /// REST API controller for managing items.
    /// Exposes endpoints for retrieving, updating, and deleting inventory items.
    /// Creation is now handled via SupplyPurchase.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemCommandService _itemCommandService;
        private readonly ItemQueryService _itemQueryService;

        public ItemController(ItemCommandService itemCommandService, ItemQueryService itemQueryService)
        {
            _itemCommandService = itemCommandService;
            _itemQueryService = itemQueryService;
        }

        /// <summary>
        /// Retrieves a list of all items in the inventory.
        /// </summary>
        /// <returns>Returns 200 OK with the list of items.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Listar Ítems", Description = "Obtiene una lista de todos los ítems registrados en el inventario.")]
        public async Task<ActionResult<IEnumerable<ItemResource>>> GetAllItems()
        {
            var getAllQuery = new GetAllItemsQuery();
            var items = await _itemQueryService.Handle(getAllQuery);
            var itemResources = ItemResourceFromEntityAssembler.ToResourceListFromEntityList(items);
            return Ok(itemResources);
        }

        /// <summary>
        /// Updates an existing item in the inventory.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="resource">The resource containing updated item data.</param>
        /// <returns>Returns 200 OK with the updated item; 400 Bad Request; 404 Not Found.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualizar Ítem", Description = "Actualiza la información de un ítem existente en el inventario.")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateCommand = UpdateItemCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updatedItem = await _itemCommandService.Handle(updateCommand);

            if (updatedItem == null)
                return NotFound();

            var itemResource = ItemResourceFromEntityAssembler.ToResourceFromEntity(updatedItem);
            return Ok(itemResource);
        }

        /// <summary>
        /// Deletes an existing item from the inventory by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>Returns 204 No Content; 404 Not Found if the item does not exist.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar Ítem", Description = "Elimina un ítem del sistema de inventario mediante su ID.")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var deleteCommand = new DeleteItemCommand(id);
            var success = await _itemCommandService.Handle(deleteCommand);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}