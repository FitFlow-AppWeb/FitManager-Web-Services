// Notifications/Application/Internal/QueryServices/MemberNotificationQueryService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using FitManager_Web_Services.Notifications.Domain.Repositories;


namespace FitManager_Web_Services.Notifications.Application.Internal.QueryServices
{
    /// <summary>
    /// Implements the <see cref="IMemberNotificationQueryService"/> contract.
    /// This service handles queries related to retrieving notifications sent to members.
    /// </summary>
    public class MemberNotificationQueryService : IMemberNotificationQueryService
    {
        private readonly IMemberNotificationRepository _memberNotificationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberNotificationQueryService"/> class.
        /// </summary>
        /// <param name="memberNotificationRepository">The repository for <see cref="MemberNotification"/> entities.</param>
        public MemberNotificationQueryService(IMemberNotificationRepository memberNotificationRepository)
        {
            _memberNotificationRepository = memberNotificationRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all notifications sent to members.
        /// </summary>
        /// <param name="query">The <see cref="GetAllMemberNotificationsQuery"/> indicating the request.</param>
        /// <returns>A collection of <see cref="MemberNotification"/> entities, with included related data.</returns>
        public async Task<IEnumerable<MemberNotification>> Handle(GetAllMemberNotificationsQuery query)
        {
            return await _memberNotificationRepository.GetAllMemberNotificationsWithDetailsAsync(); 
        }
    }
}