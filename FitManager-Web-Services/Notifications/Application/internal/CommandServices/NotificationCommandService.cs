using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Notifications.Application.Internal.CommandServices
{
    public class NotificationCommandService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationCommandService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Notification> Handle(CreateNotificationCommand command)
        {
            var notification = new Notification(command.CreatedAt, command.Title, command.Message);

            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            // Add the necessary relationships for members and employees
            // You can implement logic to add related members and employees to the notification

            return notification;
        }

        public async Task<Notification> Handle(UpdateNotificationCommand command)
        {
            var existingNotification = await _notificationRepository.GetByIdAsync(command.Id);
            if (existingNotification == null) return null;

            existingNotification.CreatedAt = command.CreatedAt;
            existingNotification.Title = command.Title;
            existingNotification.Message = command.Message;

            await _notificationRepository.UpdateAsync(existingNotification);
            await _unitOfWork.CompleteAsync();

            return existingNotification;
        }

        public async Task<bool> Handle(DeleteNotificationCommand command)
        {
            var notification = await _notificationRepository.GetByIdAsync(command.Id);
            if (notification == null) return false;

            await _notificationRepository.DeleteAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
