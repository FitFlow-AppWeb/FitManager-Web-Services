using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Notifications.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for notification-related operations.
    /// In Domain-Driven Design (DDD), domain services encapsulate business logic that
    /// doesn't naturally fit within a single aggregate, or orchestrates operations across multiple aggregates.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Asynchronously retrieves all notifications.
        /// </summary>
        Task<IEnumerable<Notification>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a notification by its unique identifier.
        /// </summary>
        Task<Notification> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously adds a new notification.
        /// </summary>
        Task AddAsync(Notification notification);

        /// <summary>
        /// Asynchronously updates an existing notification.
        /// </summary>
        Task UpdateAsync(Notification notification);

        /// <summary>
        /// Asynchronously deletes a notification by its unique identifier.
        /// </summary>
        Task DeleteAsync(int id);
    }
}