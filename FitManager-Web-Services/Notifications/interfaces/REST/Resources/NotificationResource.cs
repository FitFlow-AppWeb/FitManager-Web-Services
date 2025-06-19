namespace FitManager_Web_Services.Notifications.Interfaces.REST.Resources
{
    public record NotificationResource(
        int Id,
        DateTime CreatedAt,
        string Title,
        string Message,
        IEnumerable<NotificationMemberResource> NotificationMembers,
        IEnumerable<NotificationPersonalResource> NotificationPersonals
    );
}