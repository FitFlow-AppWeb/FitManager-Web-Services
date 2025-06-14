using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Employees.Application.Internal.CommandServices;
using FitManager_Web_Services.Employees.Application.Internal.QueryServices;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;
using FitManager_Web_Services.Employees.Interfaces.REST.Transform;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Model.Queries;

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

        // Crear un nuevo empleado
        [HttpPost]
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
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeResource.Id }, employeeResource);
        }

        // Obtener todos los empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployees()
        {
            var getAllQuery = new GetAllEmployeesQuery();
            var employees = await _employeeQueryService.Handle(getAllQuery);
            var employeeResources = EmployeeResourceFromEntityAssembler.ToResourceListFromEntityList(employees);
            return Ok(employeeResources);
        }

        // Obtener un empleado por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeeById(int id)
        {
            var getByIdQuery = new GetEmployeeByIdQuery(id);
            var employee = await _employeeQueryService.Handle(getByIdQuery);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(employee);
            return Ok(employeeResource);
        }

        // Obtener un empleado por DNI
        [HttpGet("dni/{dni}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeeByDni(int dni)
        {
            var getByDniQuery = new GetEmployeeByDniQuery(dni);
            var employee = await _employeeQueryService.Handle(getByDniQuery);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(employee);
            return Ok(employeeResource);
        }

        // Actualizar un empleado
        [HttpPut("{id}")]
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

        // Eliminar un empleado
        [HttpDelete("{id}")]
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
