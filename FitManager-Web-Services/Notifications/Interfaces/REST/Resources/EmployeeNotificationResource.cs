// Notifications/Interfaces/REST/Resources/EmployeeNotificationResource.cs

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for a notification sent to a specific employee.
    /// This DTO is used in GET responses for `/api/v1/StaffNotifications`.
    /// It includes details of the notification itself and the associated employee.
    /// </summary>
    public record EmployeeNotificationResource(
        int Id, 
        int NotificationId,
        int EmployeeId,
        string NotificationTitle, 
        string NotificationMessage, 
        string EmployeeFirstName, 
        string EmployeeLastName 
    );
}