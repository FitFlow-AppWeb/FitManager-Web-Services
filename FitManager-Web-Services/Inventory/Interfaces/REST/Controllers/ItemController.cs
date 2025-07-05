using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices;
using FitManager_Web_Services.Inventory.Application.Internal.QueryServices;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;
using FitManager_Web_Services.Inventory.Interfaces.REST.Transform;
using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Domain.Model.Queries;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Localization;
using FitManager_Web_Services.Resources;

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
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ItemController(
            ItemCommandService itemCommandService,
            ItemQueryService itemQueryService,
            IStringLocalizer<SharedResource> localizer)
        {
            _itemCommandService = itemCommandService;
            _itemQueryService = itemQueryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Retrieves a list of all items in the inventory.
        /// </summary>
        /// <returns>Returns 200 OK with the list of items.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Listar Ítems", Description = "Obtiene una lista de todos los ítems registrados en el inventario.")]
        public async Task<ActionResult<object>> GetAllItems()
        {
            var getAllQuery = new GetAllItemsQuery();
            var items = await _itemQueryService.Handle(getAllQuery);
            var itemResources = ItemResourceFromEntityAssembler.ToResourceListFromEntityList(items);

            var localizedMessage = _localizer["ItemsRetrieved"];
            return Ok(new
            {
                message = new
                {
                    name = "ItemsRetrieved",
                    value = localizedMessage.Value,
                    resourceNotFound = localizedMessage.ResourceNotFound,
                    searchedLocation = localizedMessage.SearchedLocation
                },
                data = itemResources
            });
        }


        /// <summary>
        /// Updates an existing item in the inventory.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="resource">The resource containing updated item data.</param>
        /// <returns>Returns 200 OK with the updated item; 400 Bad Request; 404 Not Found.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizar Ítem",
            Description = "Actualiza la información de un ítem existente en el inventario."
        )]
        [ProducesResponseType(typeof(ItemResource), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemResource resource)
        {
            if (!ModelState.IsValid)
            {
                var invalidMessage = _localizer["InvalidItemData"];
                return BadRequest(new
                {
                    message = new
                    {
                        name = "InvalidItemData",
                        value = invalidMessage.Value
                    },
                    errors = ModelState
                });
            }

            var updateCommand = UpdateItemCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updatedItem = await _itemCommandService.Handle(updateCommand);

            if (updatedItem == null)
            {
                var notFoundMessage = _localizer["ItemNotFound"];
                return NotFound(new
                {
                    message = new
                    {
                        name = "ItemNotFound",
                        value = notFoundMessage.Value
                    }
                });
            }

            var itemResource = ItemResourceFromEntityAssembler.ToResourceFromEntity(updatedItem);

            var successMessage = _localizer["ItemUpdated"];
            return Ok(new
            {
                message = new
                {
                    name = "ItemUpdated",
                    value = successMessage.Value
                },
                data = itemResource
            });
        }


        /// <summary>
        /// Deletes an existing item from the inventory by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>Returns 204 No Content; 404 Not Found if the item does not exist.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar Ítem", Description = "Elimina un ítem del sistema de inventario mediante su ID.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var deleteCommand = new DeleteItemCommand(id);
            var success = await _itemCommandService.Handle(deleteCommand);

            if (!success)
            {
                var notFoundMessage = _localizer["ItemNotFound"];
                return NotFound(new
                {
                    message = new
                    {
                        name = "ItemNotFound",
                        value = notFoundMessage.Value
                    }
                });
            }

            return NoContent();
        }

    }
}