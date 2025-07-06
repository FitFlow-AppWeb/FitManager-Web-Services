using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new item type via the REST API.
    /// Serves as a Data Transfer Object (DTO) for client requests.
    /// </summary>
    /// <param name="Name">The name of the new item type (e.g., "Treadmill", "Dumbbell").</param>
    /// <param name="Description">A textual description providing additional details about the item type.</param>
    public record CreateItemTypeResource(
        [Required] [StringLength(100, MinimumLength = 2)] string Name,
        [StringLength(500)] string Description
    );
}