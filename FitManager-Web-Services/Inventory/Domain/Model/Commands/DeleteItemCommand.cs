namespace FitManager_Web_Services.Inventory.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to delete an inventory item.
    /// This record is an immutable data transfer object (DTO) that carries the identifier of the item
    /// to be deleted from the application's external boundary into the Domain/Application layer
    /// for processing by a command handler (e.g., <see cref="Application.Internal.CommandServices.ItemCommandService"/>).
    /// </summary>
    /// <param name="Id">The unique identifier of the item to delete.</param>
    public record DeleteItemCommand(
        int Id
    );
}