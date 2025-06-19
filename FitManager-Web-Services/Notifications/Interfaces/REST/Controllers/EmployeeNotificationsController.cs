// Notifications/Interfaces/REST/Controllers/EmployeeNotificationsController.cs

using FitManager_Web_Services.Notifications.Application.Internal.CommandServices;
using FitManager_Web_Services.Notifications.Application.Internal.QueryServices;
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;
using FitManager_Web_Services.Notifications.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime; 
using Swashbuckle.AspNetCore.Annotations; 

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] 
    [Produces(MediaTypeNames.Application.Json)] 
    public class EmployeeNotificationsController : ControllerBase
    {
        private readonly IEmployeeNotificationCommandService _employeeNotificationCommandService;
        private readonly IEmployeeNotificationQueryService _employeeNotificationQueryService;

        public EmployeeNotificationsController(
            IEmployeeNotificationCommandService employeeNotificationCommandService,
            IEmployeeNotificationQueryService employeeNotificationQueryService)
        {
            _employeeNotificationCommandService = employeeNotificationCommandService;
            _employeeNotificationQueryService = employeeNotificationQueryService;
        }

        /// <summary>
        /// Creates a new notification and sends it to specified employees.
        /// </summary>
        /// <param name="resource">The resource containing notification details and employee IDs.</param>
        /// <returns>A status indicating success or failure of the operation.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create Employee Notification",
            Description = "Creates a new notification and associates it with a list of employees."
        )]
        [ProducesResponseType(typeof(void), 201)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)] 
        [ProducesResponseType(typeof(string), 500)] 
        public async Task<IActionResult> CreateEmployeeNotification([FromBody] CreateEmployeeNotificationResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = CreateEmployeeNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
                await _employeeNotificationCommandService.Handle(command);
                return StatusCode(201); 
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all notifications sent to employees.
        /// </summary>
        /// <returns>A list of employee notification resources.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Employee Notifications",
            Description = "Retrieves a list of all notifications that have been sent to employees, including their associated details."
        )]
        [ProducesResponseType(typeof(IEnumerable<EmployeeNotificationResource>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<IEnumerable<EmployeeNotificationResource>>> GetAllEmployeeNotifications()
        {
            try
            {
                var query = new GetAllEmployeeNotificationsQuery();
                var employeeNotifications = await _employeeNotificationQueryService.Handle(query);
                var resources = employeeNotifications.Select(EmployeeNotificationResourceFromEntityAssembler.ToResourceFromEntity);
                return Ok(resources);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}