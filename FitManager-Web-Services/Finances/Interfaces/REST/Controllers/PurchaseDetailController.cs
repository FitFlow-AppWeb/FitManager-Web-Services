using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PurchaseDetailController : ControllerBase
{
    private readonly PurchaseDetailQueryService _queryService;
    private readonly PurchaseDetailCommandService  _commandService;

    public PurchaseDetailController(PurchaseDetailQueryService queryService, PurchaseDetailCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Listar detalles de compras",
        Description = "Obtiene todos los detalles de compras de insumos"
    )]
    public async Task<ActionResult<IEnumerable<PurchaseDetailResource>>> GetAll()
    {
        var details = await _queryService.GetAllAsync();
        var resources = PurchaseDetailToResourceAssembler.ToResourceListFromEntityList(details);
        return Ok(resources);
    }
    
    
    [HttpGet("by-purchase/{purchaseId}")]
    [SwaggerOperation(
        Summary = "Listar detalles por ID de compra",
        Description = "Obtiene todos los detalles de una compra específica"
    )]
    public async Task<ActionResult<IEnumerable<PurchaseDetailResource>>> GetByPurchaseId(int purchaseId)
    {
        var details = await _queryService.GetByPurchaseIdAsync(purchaseId);
        var resources = PurchaseDetailToResourceAssembler.ToResourceListFromEntityList(details);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Registrar un detalle de compra",
        Description = "Registra un nuevo detalle dentro de una compra de insumos"
    )]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseDetailResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = PurchaseDetailFromResourceAssembler.ToEntityFromResource(resource);

        // Validación pendiente del ItemType (esperando repositorio)
        // var itemType = await _itemTypeRepository.GetByIdAsync(entity.ItemTypeId);
        // if (itemType == null) return NotFound("Tipo de ítem no encontrado");

        var result = await _commandService.CreateAsync(
            entity.SupplyPurchaseId,
            entity.ItemTypeId,
            entity.UnitPrice,
            entity.Quantity
        );

        if (result == null)
            return NotFound("Compra de insumo no encontrada.");

        return Created($"/api/v1/purchasedetail/{result.Id}", result);
    }
    
}