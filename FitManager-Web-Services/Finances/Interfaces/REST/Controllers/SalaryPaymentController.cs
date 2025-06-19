using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using FitManager_Web_Services.Finances.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SalaryPaymentController : ControllerBase
{
    private readonly SalaryPaymentCommandService _commandService;
    private readonly SalaryPaymentQueryService _queryService;

    public SalaryPaymentController(SalaryPaymentCommandService commandService, SalaryPaymentQueryService queryService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

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

        // TODO: Validar existencia del Employee cuando el BC esté disponible
        // var employee = await _employeeRepository.GetByIdAsync(entity.EmployeeId);
        // if (employee == null) return NotFound("Empleado no encontrado");

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
}