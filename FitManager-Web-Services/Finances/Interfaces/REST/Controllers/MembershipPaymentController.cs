using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

/// <summary>
/// API controller for managing membership payments.
/// </summary>
/// <remarks>
/// Provides RESTful endpoints for registering new membership payments and retrieving existing ones.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class MembershipPaymentController : ControllerBase
{
    private readonly MembershipPaymentCommandService _commandService;
    private readonly MembershipPaymentQueryService _queryService;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MembershipPaymentController"/> class.
    /// </summary>
    /// <param name="commandService">The command service for membership payment operations.</param>
    /// <param name="queryService">The query service for membership payment retrieval.</param>
    public MembershipPaymentController(
        MembershipPaymentCommandService commandService,
        MembershipPaymentQueryService queryService
        )
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    /// <summary>
    /// Registers a new payment made by a member.
    /// </summary>
    /// <param name="resource">The resource containing details for the new membership payment.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the result of the registration operation.
    /// Returns 201 Created with the created membership payment resource on success,
    /// 400 BadRequest if validation fails, or 404 NotFound if the member is not found.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Register Membership Payment",
        Description = "Registers a new payment made by a member."
    )]
    [SwaggerResponse(201, "The membership payment was registered successfully.", typeof(MembershipPaymentResource))]
    [SwaggerResponse(400, "Invalid input data.")]
    [SwaggerResponse(404, "Member not found.")]
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
        
        var createdPaymentResource = MembershipPaymentToResourceAssembler.ToResourceFromEntity(result);
        
        var resourceUri = Url.Action(null, "MembershipPayment", new { id = createdPaymentResource.Id }, Request.Scheme);
        
        return Created(resourceUri ?? $"api/v1/membershippayment/{createdPaymentResource.Id}", createdPaymentResource);
    }
    
    /// <summary>
    /// Retrieves all registered membership payments.
    /// </summary>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing an enumerable of <see cref="MembershipPaymentResource"/> objects.
    /// Returns 200 OK with the list of membership payments.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "List Membership Payments",
        Description = "Retrieves all registered membership payments."
    )]
    [SwaggerResponse(200, "A list of membership payments was retrieved successfully.", typeof(IEnumerable<MembershipPaymentResource>))]
    public async Task<ActionResult<IEnumerable<MembershipPaymentResource>>> GetAll()
    {
        var payments = await _queryService.GetAllAsync();
        var resources = MembershipPaymentToResourceAssembler.ToResourceListFromEntityList(payments);
        return Ok(resources);
    }
}