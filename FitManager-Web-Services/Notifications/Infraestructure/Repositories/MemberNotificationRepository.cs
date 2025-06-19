// Notifications/Infrastructure/Repositories/MemberNotificationRepository.cs

using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Notifications.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IMemberNotificationRepository"/> interface, providing data access
    /// operations for <see cref="MemberNotification"/> entities using Entity Framework Core.
    /// It inherits common repository functionalities from <see cref="BaseRepository{TEntity}"/>.
    /// </summary>
    public class MemberNotificationRepository : BaseRepository<MemberNotification>, IMemberNotificationRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberNotificationRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public MemberNotificationRepository(AppDbContext context) : base(context)
        {
            
        }

        /// <summary>
        /// Asynchronously finds all <see cref="MemberNotification"/> entities for a specific member.
        /// </summary>
        /// <param name="memberId">The ID of the member.</param>
        /// <returns>A collection of <see cref="MemberNotification"/> entities.</returns>
        public async Task<IEnumerable<MemberNotification>> FindByMemberIdAsync(int memberId)
        {
            return await _context.Set<MemberNotification>() 
                .Where(mn => mn.MemberId == memberId)
                .Include(mn => mn.Notification) 
                .Include(mn => mn.Member)       
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously finds a specific <see cref="MemberNotification"/> by its composite key.
        /// </summary>
        /// <param name="notificationId">The ID of the notification.</param>
        /// <param name="memberId">The ID of the member.</param>
        /// <returns>The <see cref="MemberNotification"/> entity if found, otherwise null.</returns>
        public async Task<MemberNotification?> FindByNotificationIdAndMemberIdAsync(int notificationId, int memberId)
        {
            return await _context.Set<MemberNotification>() // Use _context.Set<T>() or _entities here
                .Include(mn => mn.Notification)
                .Include(mn => mn.Member)
                .FirstOrDefaultAsync(mn => mn.NotificationId == notificationId && mn.MemberId == memberId);
        }

        /// <summary>
        /// Asynchronously retrieves all member notifications, including their related Notification and Member details.
        /// </summary>
        /// <returns>A collection of <see cref="MemberNotification"/> entities with eager loaded details.</returns>
        public async Task<IEnumerable<MemberNotification>> GetAllMemberNotificationsWithDetailsAsync() 
        {
            return await _context.Set<MemberNotification>() 
                .Include(mn => mn.Notification)
                .Include(mn => mn.Member)
                .ToListAsync();
        }
    }
}