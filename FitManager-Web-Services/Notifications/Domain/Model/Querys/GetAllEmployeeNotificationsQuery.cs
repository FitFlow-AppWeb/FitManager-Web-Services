// Notifications/Domain/Model/Queries/GetAllEmployeeNotificationQuery.cs

namespace FitManager_Web_Services.Notifications.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all notifications that have been sent to employees.
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all <see cref="EmployeeNotification"/> entities, typically including their related
    /// <see cref="Notification"/> details and <see cref="Employees.Domain.Model.Aggregates.Employee"/> information.
    /// </summary>
    public record GetAllEmployeeNotificationsQuery();
}