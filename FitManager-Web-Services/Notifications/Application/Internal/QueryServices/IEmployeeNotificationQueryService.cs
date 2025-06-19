// Notifications/Application/Internal/QueryServices/IEmployeeNotificationQueryService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates; 
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Application.Internal.QueryServices
{
    /// <summary>
    /// Defines the contract for the query service responsible for processing
    /// queries related to retrieving notifications sent to employees.
    /// </summary>
    public interface IEmployeeNotificationQueryService
    {
        /// <summary>
        /// Handles the query to retrieve all notifications sent to employees.
        /// </summary>
        /// <param name="query">The <see cref="GetAllEmployeeNotificationsQuery"/> indicating the request.</param>
        /// <returns>A collection of <see cref="EmployeeNotification"/> entities, potentially with included related data.</returns>
        Task<IEnumerable<EmployeeNotification>> Handle(GetAllEmployeeNotificationsQuery query);
    }
}