using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for updating an existing user via the REST API.
    /// </summary>
    /// <param name="Email">The new email address of the user.</param>
    /// <param name="Password">The new password of the user.</param>
    /// <param name="Icon">The new icon URL or identifier for the user.</param>
    /// <param name="Subscription">The new subscription plan for the user.</param>
    public record UpdateUserResource(
        [Required] string Email,
        string? Password, 
        [Required] string Icon,
        [Required] string Subscription
    );
}