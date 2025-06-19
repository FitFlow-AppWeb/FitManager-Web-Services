using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Employees.Application.Internal.CommandServices;
using FitManager_Web_Services.Employees.Application.Internal.QueryServices;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;
using FitManager_Web_Services.Employees.Interfaces.REST.Transform;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Model.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Controllers
{   
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeCommandService _employeeCommandService;
        private readonly EmployeeQueryService _employeeQueryService;

        public EmployeeController(EmployeeCommandService employeeCommandService, EmployeeQueryService employeeQueryService)
        {
            _employeeCommandService = employeeCommandService;
            _employeeQueryService = employeeQueryService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add Employee",
            Description = "Creates a new employee in the system."
        )]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createCommand = CreateEmployeeCommandFromResourceAssembler.ToCommandFromResource(resource);
            var employee = await _employeeCommandService.Handle(createCommand);

            if (employee == null)
            {
                return BadRequest("Could not create employee due to an internal issue.");
            }

            var employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(employee);
            return Created(string.Empty, employeeResource); 
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List All Employees",
            Description = "Retrieves a list of all employees registered in the system."
        )]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployees()
        {
            var getAllQuery = new GetAllEmployeesQuery();
            var employees = await _employeeQueryService.Handle(getAllQuery);
            var employeeResources = EmployeeResourceFromEntityAssembler.ToResourceListFromEntityList(employees);
            return Ok(employeeResources);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update Employee",
            Description = "Updates the data of an existing employee."
        )]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeResource resource) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateCommand = UpdateEmployeeCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updatedEmployee = await _employeeCommandService.Handle(updateCommand); 

            if (updatedEmployee == null)
            {
                return NotFound();
            }

            var employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(updatedEmployee);
            return Ok(employeeResource); 
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete Employee",
            Description = "Deletes an existing employee from the system by their ID."
        )]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleteCommand = new DeleteEmployeeCommand(id);
            var success = await _employeeCommandService.Handle(deleteCommand);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
