namespace FitManager_Web_Services.Notifications.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve a specific notification by its ID.
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch a notification by its ID. It is processed by a query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.NotificationQueryService"/>).
    /// </summary>
    public record GetNotificationByIdQuery(int Id);
}