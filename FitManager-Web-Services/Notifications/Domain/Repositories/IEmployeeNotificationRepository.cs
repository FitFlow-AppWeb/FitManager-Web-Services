// Notifications/Domain/Repositories/IEmployeeNotificationRepository.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository handling <see cref="EmployeeNotification"/> entities.
    /// It provides specific operations for managing notifications sent to employees.
    /// </summary>
    public interface IEmployeeNotificationRepository : IBaseRepository<EmployeeNotification>
    {
        // Add specific methods here if needed, beyond what IBaseRepository provides.
        Task<IEnumerable<EmployeeNotification>> FindByEmployeeIdAsync(int employeeId);
        Task<EmployeeNotification?> FindByNotificationIdAndEmployeeIdAsync(int notificationId, int employeeId);

        /// <summary>
        /// Asynchronously retrieves all employee notifications, including their related Notification and Employee details.
        /// </summary>
        /// <returns>A collection of <see cref="EmployeeNotification"/> entities with eager loaded details.</returns>
        Task<IEnumerable<EmployeeNotification>> GetAllEmployeeNotificationsWithDetailsAsync(); // <-- NUEVO MÉTODO
    }
}