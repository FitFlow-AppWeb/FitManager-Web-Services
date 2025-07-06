namespace FitManager_Web_Services.Inventory.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new ItemType.
    /// Encapsulates the necessary data for creating an item type within the domain.
    /// </summary>
    /// <param name="Name">The name of the item type.</param>
    /// <param name="Description">The description of the item type.</param>
    public record CreateItemTypeCommand(string Name, string Description);
}