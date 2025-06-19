// Notifications/Interfaces/REST/Resources/CreateMemberNotificationResource.cs

using System.Collections.Generic; // Required for IEnumerable
using System.ComponentModel.DataAnnotations; // Required for [Required]

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new notification intended for members.
    /// This DTO is used in POST requests to `/api/v1/MemberNotifications`.
    /// </summary>
    public record CreateMemberNotificationResource(
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        string Title,

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters.")]
        string Message,

        [Required(ErrorMessage = "At least one Member ID is required.")]
        [MinLength(1, ErrorMessage = "At least one Member ID must be provided.")]
        IEnumerable<int> MemberIds
    );
}