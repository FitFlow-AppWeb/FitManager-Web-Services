// Notifications/Application/Internal/QueryServices/EmployeeNotificationQueryService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Model.Queries;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Application.Internal.QueryServices
{
    /// <summary>
    /// Implements the <see cref="IEmployeeNotificationQueryService"/> contract.
    /// This service handles queries related to retrieving notifications sent to employees.
    /// </summary>
    public class EmployeeNotificationQueryService : IEmployeeNotificationQueryService
    {
        private readonly IEmployeeNotificationRepository _employeeNotificationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotificationQueryService"/> class.
        /// </summary>
        /// <param name="employeeNotificationRepository">The repository for <see cref="EmployeeNotification"/> entities.</param>
        public EmployeeNotificationQueryService(IEmployeeNotificationRepository employeeNotificationRepository)
        {
            _employeeNotificationRepository = employeeNotificationRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all notifications sent to employees.
        /// </summary>
        /// <param name="query">The <see cref="GetAllEmployeeNotificationsQuery"/> indicating the request.</param>
        /// <returns>A collection of <see cref="EmployeeNotification"/> entities, with included related data.</returns>
        public async Task<IEnumerable<EmployeeNotification>> Handle(GetAllEmployeeNotificationsQuery query)
        {
            return await _employeeNotificationRepository.GetAllEmployeeNotificationsWithDetailsAsync(); 
        }
    }
}