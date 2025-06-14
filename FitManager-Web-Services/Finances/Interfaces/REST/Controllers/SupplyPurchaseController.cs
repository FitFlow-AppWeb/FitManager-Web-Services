using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SupplyPurchaseController : ControllerBase
{
    private readonly SupplyPurchaseCommandService _commandService;
    private readonly SupplyPurchaseQueryService _queryService;

    public SupplyPurchaseController(SupplyPurchaseCommandService commandService, SupplyPurchaseQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Registrar una compra de insumos",
        Description = "Registra una nueva compra realizada por el gimnasio"
    )]
    public async Task<IActionResult> Create([FromBody] CreateSupplyPurchaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = SupplyPurchaseFromResourceAssembler.ToEntityFromResource(resource);
        var result = await _commandService.CreateAsync(
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.VendorName
        );

        return Created($"api/v1/supplypurchase/{result.Id}", result);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Listar compras de insumos",
        Description = "Obtiene todas las compras de insumos registradas en el sistema."
    )]
    public async Task<ActionResult<IEnumerable<SupplyPurchaseResource>>> GetAll()
    {
        var purchases = await _queryService.GetAllAsync();
        var resources = SupplyPurchaseToResourceAssembler.ToResourceListFromEntityList(purchases);
        return Ok(resources);
    }
}