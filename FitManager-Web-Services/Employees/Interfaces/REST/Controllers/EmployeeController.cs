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
    /// <summary>
    /// API controller for managing employee records.
    /// </summary>
    /// <remarks>
    /// Provides RESTful endpoints for creating, retrieving, updating, and deleting employee information.
    /// </remarks>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeCommandService _employeeCommandService;
        private readonly EmployeeQueryService _employeeQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeCommandService">The command service for employee operations.</param>
        /// <param name="employeeQueryService">The query service for employee retrieval.</param>
        public EmployeeController(EmployeeCommandService employeeCommandService, EmployeeQueryService employeeQueryService)
        {
            _employeeCommandService = employeeCommandService;
            _employeeQueryService = employeeQueryService;
        }

        /// <summary>
        /// Creates a new employee in the system.
        /// </summary>
        /// <param name="resource">The resource containing the new employee's data.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the creation operation.
        /// Returns 201 Created with the created employee resource on success, or 400 BadRequest if validation fails or employee creation encounters an issue.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Add Employee",
            Description = "Creates a new employee in the system."
        )]
        [SwaggerResponse(201, "The employee was created successfully.", typeof(EmployeeResource))]
        [SwaggerResponse(400, "Invalid input data or internal issue during creation.")]
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

        /// <summary>
        /// Retrieves a list of all employees registered in the system.
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing an enumerable of <see cref="EmployeeResource"/> objects.
        /// Returns 200 OK with the list of employees.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "List All Employees",
            Description = "Retrieves a list of all employees registered in the system."
        )]
        [SwaggerResponse(200, "A list of employees was retrieved successfully.", typeof(IEnumerable<EmployeeResource>))]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployees()
        {
            var getAllQuery = new GetAllEmployeesQuery();
            var employees = await _employeeQueryService.Handle(getAllQuery);
            var employeeResources = EmployeeResourceFromEntityAssembler.ToResourceListFromEntityList(employees);
            return Ok(employeeResources);
        }

        /// <summary>
        /// Updates the data of an existing employee.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to update.</param>
        /// <param name="resource">The resource containing the updated employee data.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the update operation.
        /// Returns 200 OK with the updated employee resource if successful, 400 BadRequest if validation fails,
        /// or 404 NotFound if the employee does not exist.
        /// </returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update Employee",
            Description = "Updates the data of an existing employee."
        )]
        [SwaggerResponse(200, "The employee was updated successfully.", typeof(EmployeeResource))]
        [SwaggerResponse(400, "Invalid input data.")]
        [SwaggerResponse(404, "Employee not found.")]
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

        /// <summary>
        /// Deletes an existing employee from the system by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the deletion operation.
        /// Returns 204 No Content on successful deletion, or 404 NotFound if the employee does not exist.
        /// </returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete Employee",
            Description = "Deletes an existing employee from the system by their ID."
        )]
        [SwaggerResponse(204, "The employee was deleted successfully.")]
        [SwaggerResponse(404, "Employee not found.")]
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