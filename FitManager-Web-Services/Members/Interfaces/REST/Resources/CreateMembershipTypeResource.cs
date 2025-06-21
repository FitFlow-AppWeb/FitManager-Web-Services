// Members/Interfaces/REST/Resources/CreateMembershipTypeResource.cs

using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new membership type.
    /// </summary>
    /// <param name="Name">The name of the membership type (e.g., "Premium", "Basic").</param>
    /// <param name="Description">A detailed description of the membership type.</param>
    /// <param name="Price">The price of the membership type.</param>
    /// <param name="Duration">The duration of the membership (e.g., "1 Month", "3 Months", "1 Year").</param>
    /// <param name="Benefits">The benefits included with this membership type.</param>
    public record CreateMembershipTypeResource(
        [Required] [StringLength(100)] string Name,
        [StringLength(500)] string Description,
        [Required] [Range(0, int.MaxValue)] int Price,
        [Required] [StringLength(50)] string Duration,
        [StringLength(1000)] string Benefits
    );
}