// Notifications/Infrastructure/Repositories/EmployeeNotificationRepository.cs

using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IEmployeeNotificationRepository"/> interface, providing data access
    /// operations for <see cref="EmployeeNotification"/> entities using Entity Framework Core.
    /// It inherits common repository functionalities from <see cref="BaseRepository{TEntity}"/>.
    /// </summary>
    public class EmployeeNotificationRepository : BaseRepository<EmployeeNotification>, IEmployeeNotificationRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotificationRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public EmployeeNotificationRepository(AppDbContext context) : base(context)
        {
            
        }

        /// <summary>
        /// Asynchronously finds all <see cref="EmployeeNotification"/> entities for a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A collection of <see cref="EmployeeNotification"/> entities.</returns>
        public async Task<IEnumerable<EmployeeNotification>> FindByEmployeeIdAsync(int employeeId)
        {
            return await _context.Set<EmployeeNotification>() 
                .Where(en => en.EmployeeId == employeeId)
                .Include(en => en.Notification) 
                .Include(en => en.Employee)     
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously finds a specific <see cref="EmployeeNotification"/> by its composite key.
        /// </summary>
        /// <param name="notificationId">The ID of the notification.</param>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>The <see cref="EmployeeNotification"/> entity if found, otherwise null.</returns>
        public async Task<EmployeeNotification?> FindByNotificationIdAndEmployeeIdAsync(int notificationId, int employeeId)
        {
            return await _context.Set<EmployeeNotification>() 
                .Include(en => en.Notification)
                .Include(en => en.Employee)
                .FirstOrDefaultAsync(en => en.NotificationId == notificationId && en.EmployeeId == employeeId);
        }

        /// <summary>
        /// Asynchronously retrieves all employee notifications, including their related Notification and Employee details.
        /// </summary>
        /// <returns>A collection of <see cref="EmployeeNotification"/> entities with eager loaded details.</returns>
        public async Task<IEnumerable<EmployeeNotification>> GetAllEmployeeNotificationsWithDetailsAsync() 
        {
            return await _context.Set<EmployeeNotification>() 
                .Include(en => en.Notification)
                .Include(en => en.Employee)
                .ToListAsync();
        }
    }
}