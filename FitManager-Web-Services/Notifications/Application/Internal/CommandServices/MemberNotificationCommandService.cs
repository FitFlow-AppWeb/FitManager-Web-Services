// Notifications/Application/Internal/CommandServices/MemberNotificationCommandService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;


namespace FitManager_Web_Services.Notifications.Application.Internal.CommandServices
{
    /// <summary>
    /// Implements the <see cref="IMemberNotificationCommandService"/> contract.
    /// This service orchestrates the creation of a new notification and its association with specified members.
    /// It interacts with domain repositories and the Unit of Work to ensure transactional consistency.
    /// </summary>
    public class MemberNotificationCommandService : IMemberNotificationCommandService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMemberNotificationRepository _memberNotificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberNotificationCommandService"/> class.
        /// </summary>
        /// <param name="notificationRepository">The repository for <see cref="Notification"/> entities.</param>
        /// <param name="memberNotificationRepository">The repository for <see cref="MemberNotification"/> entities.</param>
        /// <param name="unitOfWork">The unit of work to manage transactional operations.</param>
        public MemberNotificationCommandService(
            INotificationRepository notificationRepository,
            IMemberNotificationRepository memberNotificationRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _memberNotificationRepository = memberNotificationRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to create a new notification and send it to specified members.
        /// </summary>
        /// <param name="command">The <see cref="CreateMemberNotificationCommand"/> containing notification details and member IDs.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Handle(CreateMemberNotificationCommand command)
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

            foreach (var memberId in command.MemberIds)
            {
                var memberNotification = new MemberNotification(notification.Id, memberId);
                await _memberNotificationRepository.AddAsync(memberNotification);
            }
            
            await _unitOfWork.CompleteAsync();
        }
    }
}