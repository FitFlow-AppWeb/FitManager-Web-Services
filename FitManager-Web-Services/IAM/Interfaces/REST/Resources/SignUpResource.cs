using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the data required to register a new user.
    /// </summary>
    public record SignUpResource(
        [Required] string Email,
        [Required] string Password,
        [Required] string Icon,
        [Required] string Subscription
    );}