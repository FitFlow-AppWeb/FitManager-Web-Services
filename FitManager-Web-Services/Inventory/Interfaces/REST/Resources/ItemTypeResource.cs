namespace FitManager_Web_Services.Inventory.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource returned for an item type via the REST API.
    /// This DTO provides metadata about a specific type of inventory item.
    /// </summary>
    /// <param name="Id">The unique identifier of the item type.</param>
    /// <param name="Name">The name of the item type (e.g., "Treadmill", "Dumbbell").</param>
    /// <param name="Description">A textual description providing additional details about the item type.</param>
    public record ItemTypeResource(
        int Id,
        string Name,
        string Description
    );
}