// Notifications/Application/Internal/QueryServices/IMemberNotificationQueryService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates; // To reference MemberNotification entity
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Application.Internal.QueryServices
{
    /// <summary>
    /// Defines the contract for the query service responsible for processing
    /// queries related to retrieving notifications sent to members.
    /// </summary>
    public interface IMemberNotificationQueryService
    {
        /// <summary>
        /// Handles the query to retrieve all notifications sent to members.
        /// </summary>
        /// <param name="query">The <see cref="GetAllMemberNotificationsQuery"/> indicating the request.</param>
        /// <returns>A collection of <see cref="MemberNotification"/> entities, potentially with included related data.</returns>
        Task<IEnumerable<MemberNotification>> Handle(GetAllMemberNotificationsQuery query);
    }
}