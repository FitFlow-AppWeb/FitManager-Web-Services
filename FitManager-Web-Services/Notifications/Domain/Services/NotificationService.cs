using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Notifications.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="INotificationService"/> interface, providing concrete implementations
    /// for notification-related domain operations.
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Asynchronously retrieves all notifications from the repository.
        /// </summary>
        public Task<IEnumerable<Notification>> GetAllAsync()
        {
            return _notificationRepository.GetAllNotificationsAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a notification by its unique identifier from the repository.
        /// </summary>
        public Task<Notification> GetByIdAsync(int id)
        {
            return _notificationRepository.GetNotificationByIdAsync(id);
        }

        /// <summary>
        /// Asynchronously adds a new notification to the repository and commits the changes.
        /// </summary>
        public async Task AddAsync(Notification notification)
        {
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing notification in the repository and commits the changes.
        /// </summary>
        public async Task UpdateAsync(Notification notification)
        {
            await _notificationRepository.UpdateAsync(notification);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously deletes a notification by its unique identifier from the repository and commits the changes.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            await _notificationRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
