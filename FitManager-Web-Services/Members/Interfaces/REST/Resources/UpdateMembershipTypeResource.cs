// Members/Interfaces/REST/Resources/UpdateMembershipTypeResource.cs

using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for updating an existing membership type.
    /// </summary>
    /// <param name="Name">The new name of the membership type.</param>
    /// <param name="Description">The new detailed description of the membership type.</param>
    /// <param name="Price">The new price of the membership type.</param>
    /// <param name="Duration">The new duration of the membership.</param>
    /// <param name="Benefits">The new benefits included with this membership type.</param>
    public record UpdateMembershipTypeResource(
        [Required] [StringLength(100)] string Name,
        [StringLength(500)] string Description,
        [Required] [Range(0, int.MaxValue)] int Price,
        [Required] [StringLength(50)] string Duration,
        [StringLength(1000)] string Benefits
    );
}