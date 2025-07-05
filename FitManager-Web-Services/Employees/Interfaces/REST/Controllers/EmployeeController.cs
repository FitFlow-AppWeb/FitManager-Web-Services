using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Employees.Application.Internal.CommandServices;
using FitManager_Web_Services.Employees.Application.Internal.QueryServices;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;
using FitManager_Web_Services.Employees.Interfaces.REST.Transform;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Model.Queries;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Localization;
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IStringLocalizer<SharedResource> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeCommandService">The command service for employee operations.</param>
        /// <param name="employeeQueryService">The query service for employee retrieval.</param>
        public EmployeeController(
            EmployeeCommandService employeeCommandService,
            EmployeeQueryService employeeQueryService,
            IStringLocalizer<SharedResource> localizer)
        {
            _employeeCommandService = employeeCommandService;
            _employeeQueryService = employeeQueryService;
            _localizer = localizer;
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
        [Authorize]
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
                var message = _localizer["EmployeeCreationFailed"];
                return BadRequest(new { message });
            }

            var employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(employee);
            var messageSuccess = _localizer["EmployeeCreated"];

            return Created(string.Empty, new
            {
                message = new
                {
                    name = "EmployeeCreated",
                    value = messageSuccess
                },
                data = employeeResource
            });
        }


        /// <summary>
        /// Retrieves a list of all employees registered in the system.
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing an enumerable of <see cref="EmployeeResource"/> objects.
        /// Returns 200 OK with the list of employees.
        /// </returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllEmployees()
        {
            var getAllQuery = new GetAllEmployeesQuery();
            var employees = await _employeeQueryService.Handle(getAllQuery);
            var employeeResources = EmployeeResourceFromEntityAssembler.ToResourceListFromEntityList(employees);

            var message = _localizer["EmployeesRetrieved"];

            return Ok(new
            {
                message = new
                {
                    name = "EmployeesRetrieved",
                    value = message
                },
                data = employeeResources
            });
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
        [Authorize]
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
                var notFoundMessage = _localizer["EmployeeNotFound"];
                return NotFound(new
                {
                    message = new
                    {
                        name = "EmployeeNotFound",
                        value = notFoundMessage
                    }
                });
            }

            var employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(updatedEmployee);
            var successMessage = _localizer["EmployeeUpdated"];

            return Ok(new
            {
                message = new
                {
                    name = "EmployeeUpdated",
                    value = successMessage
                },
                data = employeeResource
            });
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
        [Authorize]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleteCommand = new DeleteEmployeeCommand(id);
            var success = await _employeeCommandService.Handle(deleteCommand);

            if (!success)
            {
                var notFoundMessage = _localizer["EmployeeNotFound"];
                return NotFound(new
                {
                    message = new
                    {
                        name = "EmployeeNotFound",
                        value = notFoundMessage
                    }
                });
            }

            var deletedMessage = _localizer["EmployeeDeleted"];
            return Ok(new
            {
                message = new
                {
                    name = "EmployeeDeleted",
                    value = deletedMessage
                }
            });
        }
    }
}