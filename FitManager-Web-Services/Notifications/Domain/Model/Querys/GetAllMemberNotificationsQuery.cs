// Notifications/Domain/Model/Queries/GetAllMemberNotificationsQuery.cs

namespace FitManager_Web_Services.Notifications.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all notifications that have been sent to members.
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all <see cref="MemberNotification"/> entities, typically including their related
    /// <see cref="Notification"/> details and <see cref="Members.Domain.Model.Aggregates.Member"/> information.
    /// </summary>
    public record GetAllMemberNotificationsQuery();
}