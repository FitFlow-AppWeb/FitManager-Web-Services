using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MembershipPaymentController : ControllerBase
{
    private readonly MembershipPaymentCommandService _commandService;
    private readonly MembershipPaymentQueryService _queryService;
    
    public MembershipPaymentController(
        MembershipPaymentCommandService commandService,
        MembershipPaymentQueryService queryService
        )
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Registrar un pago de membresia",
        Description = "Registrar un nuevo pago realizado por un miembro"
    )]
    public async Task<IActionResult> Create([FromBody] CreateMembershipPaymentResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var entity = MembershipPaymentFromResourceAssembler.ToEntityFromResource(resource);
        var result = await _commandService.CreateAsync(
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.MemberId
        );

        if (result == null)
            return NotFound("Miembro no encontrado.");

        return Created($"api/v1/membershippayment/{result.Id}", result);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Listar pagos de membresía",
        Description = "Obtiene todos los pagos de membresía registrados."
    )]
    public async Task<ActionResult<IEnumerable<MembershipPaymentResource>>> GetAll()
    {
        var payments = await _queryService.GetAllAsync();
        var resources = MembershipPaymentToResourceAssembler.ToResourceListFromEntityList(payments);
        return Ok(resources);
    }
}