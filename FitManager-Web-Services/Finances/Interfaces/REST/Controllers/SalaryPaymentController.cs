using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

/// <summary>
/// API controller for managing salary payments.
/// </summary>
/// <remarks>
/// Provides RESTful endpoints for registering new salary payments and retrieving existing ones,
/// including filtering by employee.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class SalaryPaymentController : ControllerBase
{
    private readonly SalaryPaymentCommandService _commandService;
    private readonly SalaryPaymentQueryService _queryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalaryPaymentController"/> class.
    /// </summary>
    /// <param name="commandService">The command service for salary payment operations.</param>
    /// <param name="queryService">The query service for salary payment retrieval.</param>
    public SalaryPaymentController(SalaryPaymentCommandService commandService, SalaryPaymentQueryService queryService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    /// <summary>
    /// Registers a new salary payment to an employee.
    /// </summary>
    /// <param name="resource">The resource containing details for the new salary payment.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the result of the registration operation.
    /// Returns 201 Created with the created salary payment entity on success,
    /// 400 BadRequest if validation fails, or 404 NotFound if the employee is not found.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Register Salary Payment",
        Description = "Registers a new salary payment to an employee."
    )]
    public async Task<IActionResult> Create([FromBody] CreateSalaryPaymentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = SalaryPaymentFromResourceAssembler.ToEntityFromResource(resource);

        var result = await _commandService.CreateAsync(
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.EmployeeId
        );

        if (result == null)
            return NotFound("Empleado no encontrado.");

        return Created($"/api/v1/salarypayment/{result.Id}", result);
    }
    
    /// <summary>
    /// Retrieves all salary payments made to employees.
    /// </summary>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing an enumerable of <see cref="SalaryPaymentResource"/> objects.
    /// Returns 200 OK with the list of salary payments.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "List Salary Payments",
        Description = "Retrieves all payments made to employees."
    )]
    public async Task<ActionResult<IEnumerable<SalaryPaymentResource>>> GetAll()
    {
        var payments = await _queryService.GetAllAsync();
        var resources = SalaryPaymentToResourceAssembler.ToResourceListFromEntityList(payments);
        return Ok(resources);
    }
    
    /// <summary>
    /// Retrieves all salary payments made to a specific employee.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a list of <see cref="SalaryPaymentResource"/> objects
    /// for the specified employee. Returns 200 OK with the list.
    /// </returns>
    [HttpGet("by-employee/{employeeId}")]
    [SwaggerOperation(
        Summary = "List Payments by Employee",
        Description = "Retrieves all salary payments made to a specific employee."
    )]
    public async Task<IActionResult> GetByEmployeeId(int employeeId)
    {
        var payments = await _queryService.GetByEmployeeIdAsync(employeeId);
        var resources = SalaryPaymentToResourceAssembler.ToResourceListFromEntityList(payments);
        return Ok(resources);
    }
}