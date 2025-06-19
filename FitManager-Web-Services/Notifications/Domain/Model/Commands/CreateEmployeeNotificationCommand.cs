// Notifications/Domain/Model/Commands/CreateEmployeeNotificationCommand.cs

using System.Collections.Generic; // Required for IEnumerable

namespace FitManager_Web_Services.Notifications.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new notification and associate it with a collection of employees.
    /// This command encapsulates the notification content and the IDs of the employees to notify.
    /// </summary>
    /// <param name="Title">The title of the notification.</param>
    /// <param name="Message">The main message content of the notification.</param>
    /// <param name="EmployeeIds">A collection of IDs of the employees who will receive this notification.</param>
    public record CreateEmployeeNotificationCommand(
        string Title,
        string Message,
        IEnumerable<int> EmployeeIds
    );
}