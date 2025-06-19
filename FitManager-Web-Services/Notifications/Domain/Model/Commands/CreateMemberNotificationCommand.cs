// Notifications/Domain/Model/Commands/CreateMemberNotificationCommand.cs

using System.Collections.Generic; // Required for IEnumerable

namespace FitManager_Web_Services.Notifications.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new notification and associate it with a collection of members.
    /// This command encapsulates the notification content and the IDs of the members to notify.
    /// </summary>
    /// <param name="Title">The title of the notification.</param>
    /// <param name="Message">The main message content of the notification.</param>
    /// <param name="MemberIds">A collection of IDs of the members who will receive this notification.</param>
    public record CreateMemberNotificationCommand(
        string Title,
        string Message,
        IEnumerable<int> MemberIds
    );
}