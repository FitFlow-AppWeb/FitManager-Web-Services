// Notifications/Application/Internal/CommandServices/EmployeeNotificationCommandService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;


namespace FitManager_Web_Services.Notifications.Application.Internal.CommandServices
{
    /// <summary>
    /// Implements the <see cref="IEmployeeNotificationCommandService"/> contract.
    /// This service orchestrates the creation of a new notification and its association with specified employees.
    /// It interacts with domain repositories and the Unit of Work to ensure transactional consistency.
    /// </summary>
    public class EmployeeNotificationCommandService : IEmployeeNotificationCommandService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeNotificationRepository _employeeNotificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotificationCommandService"/> class.
        /// </summary>
        /// <param name="notificationRepository">The repository for <see cref="Notification"/> entities.</param>
        /// <param name="employeeNotificationRepository">The repository for <see cref="EmployeeNotification"/> entities.</param>
        /// <param name="unitOfWork">The unit of work to manage transactional operations.</param>
        public EmployeeNotificationCommandService(
            INotificationRepository notificationRepository,
            IEmployeeNotificationRepository employeeNotificationRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _employeeNotificationRepository = employeeNotificationRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to create a new notification and send it to specified employees.
        /// </summary>
        /// <param name="command">The <see cref="CreateEmployeeNotificationCommand"/> containing notification details and employee IDs.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Handle(CreateEmployeeNotificationCommand command)
        {
            var notification = new Notification(
                DateTime.UtcNow, 
                command.Title,
                command.Message
            );

            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync(); 

            if (notification.Id == 0) 
            {
                throw new InvalidOperationException("Notification ID was not generated after creation. Transaction failed.");
            }

            foreach (var employeeId in command.EmployeeIds)
            {
                var employeeNotification = new EmployeeNotification(notification.Id, employeeId);
                await _employeeNotificationRepository.AddAsync(employeeNotification);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}