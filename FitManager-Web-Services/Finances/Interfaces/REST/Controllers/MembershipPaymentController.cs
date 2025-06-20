using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
// Asegúrate de tener FitManager_Web_Services.Finances.Interfaces.REST.Requests si usas CreateMembershipPaymentResource en FromBody

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
        Summary = "Register Membership Payment",
        Description = "Registers a new payment made by a member."
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

        // --- ¡MODIFICACIÓN CLAVE AQUÍ! ---
        // 1. Mapea la entidad 'result' (MembershipPayment) a tu DTO simplificado.
        var createdPaymentResource = MembershipPaymentToResourceAssembler.ToResourceFromEntity(result);

        // 2. Devuelve un 201 Created.
        //    Puedes construir la URI de ubicación manualmente si es necesario,
        //    o simplemente devolver un Created con el objeto y una URI vacía.
        //    La ruta recomendada es devolver la URI del recurso recién creado.
        var resourceUri = Url.Action(null, "MembershipPayment", new { id = createdPaymentResource.Id }, Request.Scheme);
        
        // Opción A: Ideal - Devuelve 201 Created con URI de ubicación y el objeto
        // Necesitarías que el nombre del controlador sin "Controller" sea "MembershipPayment"
        // y que el método GET por ID, si existiera, se llamara GetById.
        // Como no tienes GetById, creamos la URI manualmente o usamos una alternativa.
        
        // Opción B: Más simple si no tienes GetById y no necesitas una URI perfecta
        // Esto devolverá un 201 OK con el objeto en el cuerpo, pero sin la cabecera Location.
        // return StatusCode(201, createdPaymentResource); 
        
        // Opción C: Construyendo la URI si no hay GetById
        // Esto es lo más cercano a CreatedAtAction sin tener GetById explícito.
        // Asume que tu endpoint de GET para un solo item sería /api/v1/MembershipPayment/{id}
        // y que el nombre de tu controlador es "MembershipPaymentController"
        // (Url.Action infiere esto de los atributos [Route] del controlador).
        return Created(resourceUri ?? $"api/v1/membershippayment/{createdPaymentResource.Id}", createdPaymentResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "List Membership Payments",
        Description = "Retrieves all registered membership payments."
    )]
    public async Task<ActionResult<IEnumerable<MembershipPaymentResource>>> GetAll()
    {
        var payments = await _queryService.GetAllAsync();
        var resources = MembershipPaymentToResourceAssembler.ToResourceListFromEntityList(payments);
        return Ok(resources);
    }
}