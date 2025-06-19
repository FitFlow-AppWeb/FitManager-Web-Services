namespace FitManager_Web_Services.Notifications.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all notifications.
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all available notifications. It is processed by a query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.NotificationQueryService"/>).
    /// </summary>
    public record GetAllNotificationsQuery();
}