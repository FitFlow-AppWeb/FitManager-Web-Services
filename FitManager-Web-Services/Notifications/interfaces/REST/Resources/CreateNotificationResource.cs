namespace FitManager_Web_Services.Notifications.Interfaces.REST.Resources
{
    public record CreateNotificationResource(
        DateTime CreatedAt,
        string Title,
        string Message,
        IEnumerable<int> MemberIds,
        IEnumerable<int> EmployeeIds
    );
}