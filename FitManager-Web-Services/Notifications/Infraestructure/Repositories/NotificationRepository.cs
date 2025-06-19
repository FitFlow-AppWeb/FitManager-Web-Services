// Notifications/Infrastructure/Repositories/NotificationRepository.cs

using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; 

namespace FitManager_Web_Services.Notifications.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="INotificationRepository"/> interface, providing data access
    /// operations for the <see cref="Notification"/> aggregate using Entity Framework Core.
    /// It inherits common repository functionalities from <see cref="BaseRepository{TEntity}"/>.
    /// </summary>
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        // The constructor correctly passes the AppDbContext to the base class.
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public NotificationRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Asynchronously retrieves all notifications, including their associated member and employee notifications.
        /// </summary>
        /// <returns>A collection of <see cref="Notification"/> entities.</returns>
        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {

            return await _context.Set<Notification>() 
                .Include(n => n.MemberNotifications)   
                .Include(n => n.EmployeeNotifications) 
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a specific notification by its ID, including its associated member and employee notifications.
        /// </summary>
        /// <param name="id">The ID of the notification to retrieve.</param>
        /// <returns>The <see cref="Notification"/> entity if found, otherwise null.</returns>
        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _context.Set<Notification>() 
                .Include(n => n.MemberNotifications)   
                .Include(n => n.EmployeeNotifications)  
                .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}