using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Notifications.Application.Internal.CommandServices;
using FitManager_Web_Services.Notifications.Application.Internal.QueryServices;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationCommandService _notificationCommandService;
        private readonly NotificationQueryService _notificationQueryService;

        public NotificationController(NotificationCommandService notificationCommandService, NotificationQueryService notificationQueryService)
        {
            _notificationCommandService = notificationCommandService;
            _notificationQueryService = notificationQueryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationResource resource)
        {
            var command = new CreateNotificationCommand(
                resource.CreatedAt,
                resource.Title,
                resource.Message,
                resource.MemberIds,
                resource.EmployeeIds
            );

            var notification = await _notificationCommandService.Handle(command);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationResource>>> GetAllNotifications()
        {
            var query = new GetAllNotificationsQuery();
            var notifications = await _notificationQueryService.Handle(query);
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationResource>> GetNotificationById(int id)
        {
            var query = new GetNotificationByIdQuery(id);
            var notification = await _notificationQueryService.Handle(query);

            if (notification == null)
                return NotFound();

            return Ok(notification);
        }
    }
}
