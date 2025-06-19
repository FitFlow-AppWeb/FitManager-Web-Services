// Notifications/Domain/Repositories/IMemberNotificationRepository.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository handling <see cref="MemberNotification"/> entities.
    /// It provides specific operations for managing notifications sent to members.
    /// </summary>
    public interface IMemberNotificationRepository : IBaseRepository<MemberNotification>
    {
        // Add specific methods here if needed, beyond what IBaseRepository provides.
        Task<IEnumerable<MemberNotification>> FindByMemberIdAsync(int memberId);
        Task<MemberNotification?> FindByNotificationIdAndMemberIdAsync(int notificationId, int memberId);

        /// <summary>
        /// Asynchronously retrieves all member notifications, including their related Notification and Member details.
        /// </summary>
        /// <returns>A collection of <see cref="MemberNotification"/> entities with eager loaded details.</returns>
        Task<IEnumerable<MemberNotification>> GetAllMemberNotificationsWithDetailsAsync(); // <-- NUEVO MÉTODO
    }
}