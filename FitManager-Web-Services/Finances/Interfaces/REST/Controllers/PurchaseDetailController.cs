using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PurchaseDetailController : ControllerBase
{
    private readonly PurchaseDetailCommandService _commandService;
    private readonly PurchaseDetailQueryService _queryService;

    public PurchaseDetailController(PurchaseDetailCommandService commandService, PurchaseDetailQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "List Purchase Details", Description = "Retrieves all supply purchase details.")]
    public async Task<ActionResult<IEnumerable<PurchaseDetailResource>>> GetAll()
    {
        var entities = await _queryService.GetAllAsync();
        var resources = PurchaseDetailToResourceAssembler.ToResourceListFromEntityList(entities);
        return Ok(resources);
    }

    [HttpGet("by-purchase/{purchaseId}")]
    [SwaggerOperation(Summary = "List Details by Purchase ID", Description = "Retrieves details for a specific purchase.")]
    public async Task<ActionResult<IEnumerable<PurchaseDetailResource>>> GetByPurchaseId(int purchaseId)
    {
        var entities = await _queryService.GetByPurchaseIdAsync(purchaseId);
        var resources = PurchaseDetailToResourceAssembler.ToResourceListFromEntityList(entities);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Register Purchase Detail", Description = "Creates a new purchase detail associated with a supply purchase.")]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseDetailResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        

        var result = await _commandService.CreateAsync(
            supplyPurchaseId: 0, 
            resource.ItemTypeId,
            resource.UnitPrice,
            resource.Quantity
        );

        if (result == null)
            return NotFound("No se encontró la compra o el tipo de ítem.");

        return Created($"/api/v1/purchasedetail/{result.Id}", result);
    }

}
