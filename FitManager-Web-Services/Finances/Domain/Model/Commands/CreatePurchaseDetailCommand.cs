namespace FitManager_Web_Services.Finances.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to create a new purchase detail (line item for a supply purchase).
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that carries all the necessary
    /// information from the application's external boundary (e.g., REST API) into the Domain/Application layer
    /// for processing by a command handler (e.g., a <see cref="Application.Internal.CommandServices.PurchaseDetailCommandService"/>,
    /// if one exists, or part of a <see cref="Application.Internal.CommandServices.SupplyPurchaseCommandService"/>'s logic).
    /// It ensures that all required data for creating a new purchase detail is encapsulated.
    /// </remarks>
    /// <param name="UnitPrice">The unit price of the item being purchased in this detail.</param>
    /// <param name="Quantity">The quantity of the item being purchased.</param>
    /// <param name="SupplyPurchaseId">The unique identifier of the parent supply purchase to which this detail belongs.</param>
    /// <param name="ItemTypeId">The unique identifier of the item type being purchased.</param>
    public record CreatePurchaseDetailCommand(
        float UnitPrice,
        int Quantity,
        int SupplyPurchaseId,
        int ItemTypeId
    );
}