namespace FitManager_Web_Services.Notifications.Domain.Model.Commands
{
    public record CreateNotificationCommand(
        DateTime CreatedAt,
        string Title,
        string Message,
        IEnumerable<int> MemberIds,
        IEnumerable<int> EmployeeIds
    );
}