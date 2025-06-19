// Notifications/Interfaces/REST/Controllers/MemberNotificationsController.cs

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
    public class MemberNotificationsController : ControllerBase
    {
        private readonly IMemberNotificationCommandService _memberNotificationCommandService;
        private readonly IMemberNotificationQueryService _memberNotificationQueryService;

        public MemberNotificationsController(
            IMemberNotificationCommandService memberNotificationCommandService,
            IMemberNotificationQueryService memberNotificationQueryService)
        {
            _memberNotificationCommandService = memberNotificationCommandService;
            _memberNotificationQueryService = memberNotificationQueryService;
        }

        /// <summary>
        /// Creates a new notification and sends it to specified members.
        /// </summary>
        /// <param name="resource">The resource containing notification details and member IDs.</param>
        /// <returns>A status indicating success or failure of the operation.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create Member Notification",
            Description = "Creates a new notification and associates it with a list of members."
        )]
        [ProducesResponseType(typeof(void), 201)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)] 
        [ProducesResponseType(typeof(string), 500)] 
        public async Task<IActionResult> CreateMemberNotification([FromBody] CreateMemberNotificationResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = CreateMemberNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
                await _memberNotificationCommandService.Handle(command);
                return StatusCode(201); 
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all notifications sent to members.
        /// </summary>
        /// <returns>A list of member notification resources.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Member Notifications",
            Description = "Retrieves a list of all notifications that have been sent to members, including their associated details."
        )]
        [ProducesResponseType(typeof(IEnumerable<MemberNotificationResource>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<IEnumerable<MemberNotificationResource>>> GetAllMemberNotifications()
        {
            try
            {
                var query = new GetAllMemberNotificationsQuery();
                var memberNotifications = await _memberNotificationQueryService.Handle(query);
                var resources = memberNotifications.Select(MemberNotificationResourceFromEntityAssembler.ToResourceFromEntity);
                return Ok(resources);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}