using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices;
using FitManager_Web_Services.Inventory.Application.Internal.QueryServices;
using FitManager_Web_Services.Inventory.Domain.Model.Queries;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;
using FitManager_Web_Services.Inventory.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Localization; 
using FitManager_Web_Services.Resources; 

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ItemTypesController : ControllerBase
    {
        private readonly ItemTypeCommandService _itemTypeCommandService;
        private readonly ItemTypeQueryService _itemTypeQueryService;
        private readonly IStringLocalizer<SharedResource> _localizer; 

        public ItemTypesController(
            ItemTypeCommandService itemTypeCommandService,
            ItemTypeQueryService itemTypeQueryService,
            IStringLocalizer<SharedResource> localizer) 
        {
            _itemTypeCommandService = itemTypeCommandService;
            _itemTypeQueryService = itemTypeQueryService;
            _localizer = localizer; 
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar Tipos de Ítem", Description = "Obtiene una lista de todos los tipos de ítem registrados.")]
        public async Task<ActionResult<object>> GetAllItemTypes()  
        {
            var getAllQuery = new GetAllItemTypesQuery();
            var itemTypes = await _itemTypeQueryService.Handle(getAllQuery);
            var itemTypeResources = ItemTypeResourceFromEntityAssembler.ToResourceListFromEntityList(itemTypes);

            var localizedMessage = _localizer["ItemTypesRetrievedSuccessfully"];
            return Ok(new
            {
                message = new
                {
                    name = "ItemTypesRetrievedSuccessfully",
                    value = localizedMessage.Value,
                    resourceNotFound = localizedMessage.ResourceNotFound,
                    searchedLocation = localizedMessage.SearchedLocation
                },
                data = itemTypeResources
            });
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear Tipo de Ítem", Description = "Crea un nuevo tipo de ítem en el sistema.")]
        public async Task<IActionResult> CreateItemType([FromBody] CreateItemTypeResource resource)
        {
            if (!ModelState.IsValid)
            {
                var invalidMessage = _localizer["InvalidItemTypeData"]; 
                return BadRequest(new
                {
                    message = new
                    {
                        name = "InvalidItemTypeData",
                        value = invalidMessage.Value,
                        resourceNotFound = invalidMessage.ResourceNotFound,
                        searchedLocation = invalidMessage.SearchedLocation
                    },
                    errors = ModelState 
                });
            }

            var createCommand = CreateItemTypeCommandFromResourceAssembler.ToCommandFromResource(resource);

            var createdItemType = await _itemTypeCommandService.Handle(createCommand);

            if (createdItemType == null)
            {
                var failedMessage = _localizer["FailedToCreateItemType"]; 
                return BadRequest(new
                {
                    message = new
                    {
                        name = "FailedToCreateItemType",
                        value = failedMessage.Value,
                        resourceNotFound = failedMessage.ResourceNotFound,
                        searchedLocation = failedMessage.SearchedLocation
                    }
                });
            }

            var itemTypeResource = ItemTypeResourceFromEntityAssembler.ToResourceFromEntity(createdItemType);
            var successMessage = _localizer["ItemTypeCreatedSuccessfully"]; 
            return CreatedAtAction(nameof(GetAllItemTypes), new { id = itemTypeResource?.Id }, new
            {
                message = new
                {
                    name = "ItemTypeCreatedSuccessfully",
                    value = successMessage.Value,
                    resourceNotFound = successMessage.ResourceNotFound,
                    searchedLocation = successMessage.SearchedLocation
                },
                data = itemTypeResource
            });
        }
    }
}