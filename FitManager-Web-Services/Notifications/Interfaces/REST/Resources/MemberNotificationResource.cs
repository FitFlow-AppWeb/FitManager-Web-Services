// Notifications/Interfaces/REST/Resources/MemberNotificationResource.cs

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for a notification sent to a specific member.
    /// This DTO is used in GET responses for `/api/v1/MemberNotifications`.
    /// It includes details of the notification itself and the associated member.
    /// </summary>
    public record MemberNotificationResource(
        int Id, 
        int NotificationId,
        int MemberId,
        string NotificationTitle, 
        string NotificationMessage, 
        string MemberFirstName, 
        string MemberLastName 
    );
}