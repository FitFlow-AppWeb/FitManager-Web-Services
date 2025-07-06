// Notifications/Interfaces/REST/Controllers/EmployeeNotificationsController.cs

using FitManager_Web_Services.Notifications.Application.Internal.CommandServices;
using FitManager_Web_Services.Notifications.Application.Internal.QueryServices;
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;
using FitManager_Web_Services.Notifications.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime; 
using Swashbuckle.AspNetCore.Annotations; 
using Microsoft.Extensions.Localization;
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] 
    [Produces(MediaTypeNames.Application.Json)] 
    public class EmployeeNotificationsController : ControllerBase
    {
        private readonly IEmployeeNotificationCommandService _employeeNotificationCommandService;
        private readonly IEmployeeNotificationQueryService _employeeNotificationQueryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public EmployeeNotificationsController(
            IEmployeeNotificationCommandService employeeNotificationCommandService,
            IEmployeeNotificationQueryService employeeNotificationQueryService,
            IStringLocalizer<SharedResource> localizer)
        {
            _employeeNotificationCommandService = employeeNotificationCommandService;
            _employeeNotificationQueryService = employeeNotificationQueryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Creates a new notification and sends it to specified employees.
        /// </summary>
        /// <param name="resource">The resource containing notification details and employee IDs.</param>
        /// <returns>A status indicating success or failure of the operation.</returns>
        [HttpPost]
        [Authorize]
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

                var localizedMessage = _localizer["EmployeeNotificationCreated"];
                return Ok(new
                {
                    message = new
                    {
                        name = "EmployeeNotificationCreated",
                        value = localizedMessage.Value,
                        resourceNotFound = localizedMessage.ResourceNotFound,
                        searchedLocation = localizedMessage.SearchedLocation
                    }
                });
            }
            catch (Exception ex)
            {
                // Puedes devolver un mensaje localizado de error si quieres
                var errorMessage = _localizer["EmployeeNotificationCreationFailed"];
                return StatusCode(500, new
                {
                    message = new
                    {
                        name = "EmployeeNotificationCreationFailed",
                        value = errorMessage.Value,
                        details = ex.Message
                    }
                });
            }
        }


        /// <summary>
        /// Gets all notifications sent to employees.
        /// </summary>
        /// <returns>A list of employee notification resources.</returns>
        [HttpGet]
        [Authorize]
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

                var localizedMessage = _localizer["EmployeeNotificationsRetrieved"];
                return Ok(new
                {
                    message = new
                    {
                        name = "EmployeeNotificationsRetrieved",
                        value = localizedMessage.Value,
                        resourceNotFound = localizedMessage.ResourceNotFound,
                        searchedLocation = localizedMessage.SearchedLocation
                    },
                    data = resources
                });
            }
            catch (Exception ex)
            {
                var errorMessage = _localizer["EmployeeNotificationsRetrievalFailed"];
                return StatusCode(500, new
                {
                    message = new
                    {
                        name = "EmployeeNotificationsRetrievalFailed",
                        value = errorMessage.Value,
                        details = ex.Message
                    }
                });
            }
        }
    }
}