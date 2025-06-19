// Notifications/Application/Internal/CommandServices/IMemberNotificationCommandService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Application.Internal.CommandServices
{
    /// <summary>
    /// Defines the contract for the command service responsible for processing
    /// commands related to sending notifications to members.
    /// This service orchestrates the creation of <see cref="Domain.Model.Aggregates.Notification"/>
    /// and <see cref="Domain.Model.Aggregates.MemberNotification"/> entities.
    /// </summary>
    public interface IMemberNotificationCommandService
    {
        /// <summary>
        /// Handles the command to create a new notification and send it to specified members.
        /// </summary>
        /// <param name="command">The <see cref="CreateMemberNotificationCommand"/> containing notification details and member IDs.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Handle(CreateMemberNotificationCommand command);
    }
}