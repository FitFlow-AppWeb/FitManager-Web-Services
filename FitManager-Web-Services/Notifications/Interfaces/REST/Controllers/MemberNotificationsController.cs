// Notifications/Interfaces/REST/Controllers/MemberNotificationsController.cs

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
    public class MemberNotificationsController : ControllerBase
    {
        private readonly IMemberNotificationCommandService _memberNotificationCommandService;
        private readonly IMemberNotificationQueryService _memberNotificationQueryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public MemberNotificationsController(
            IMemberNotificationCommandService memberNotificationCommandService,
            IMemberNotificationQueryService memberNotificationQueryService,
            IStringLocalizer<SharedResource> localizer)
        {
            _memberNotificationCommandService = memberNotificationCommandService;
            _memberNotificationQueryService = memberNotificationQueryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Creates a new notification and sends it to specified members.
        /// </summary>
        /// <param name="resource">The resource containing notification details and member IDs.</param>
        /// <returns>A status indicating success or failure of the operation.</returns>
        [HttpPost]
        [Authorize]
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
                var validationMessage = _localizer["InvalidData"];
                return BadRequest(new { message = validationMessage });
            }

            try
            {
                var command = CreateMemberNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
                await _memberNotificationCommandService.Handle(command);
                var message = _localizer["MemberNotificationCreated"];
                return StatusCode(201, new { message });
            }
            catch (System.Exception ex)
            {
                var errorMessage = _localizer["InternalServerError"];
                return StatusCode(500, new { message = errorMessage, detail = ex.Message });
            }
        }

        /// <summary>
        /// Gets all notifications sent to members.
        /// </summary>
        /// <returns>A list of member notification resources.</returns>
        [HttpGet]
        [Authorize]
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
                var message = _localizer["MemberNotificationsRetrieved"];
                return Ok(new { message, data = resources });
            }
            catch (System.Exception ex)
            {
                var errorMessage = _localizer["InternalServerError"];
                return StatusCode(500, new { message = errorMessage, detail = ex.Message });
            }
        }
    }
}