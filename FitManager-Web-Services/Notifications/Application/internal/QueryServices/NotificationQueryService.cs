using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Application.Internal.QueryServices
{
    public class NotificationQueryService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationQueryService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all notifications.
        /// </summary>
        /// <param name="query">The query object that contains parameters for fetching notifications.</param>
        /// <returns>A list of notifications.</returns>
        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query)
        {
            return await _notificationRepository.GetAllNotificationsAsync();
        }

        /// <summary>
        /// Handles the query to retrieve a specific notification by its ID.
        /// </summary>
        /// <param name="query">The query object that contains the ID of the notification.</param>
        /// <returns>The notification with the specified ID.</returns>
        public async Task<Notification?> Handle(GetNotificationByIdQuery query)
        {
            return await _notificationRepository.GetNotificationByIdAsync(query.Id);
        }
    }
}