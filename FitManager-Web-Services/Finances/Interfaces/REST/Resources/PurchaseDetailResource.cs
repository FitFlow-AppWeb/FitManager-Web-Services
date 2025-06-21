namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for a purchase detail, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the purchase detail.</param>
/// <param name="SupplyPurchaseId">The unique identifier of the main supply purchase to which this detail belongs.</param>
/// <param name="ItemTypeId">The unique identifier of the type of item purchased in this detail.</param>
/// <param name="UnitPrice">The price per unit of the item as recorded in this detail.</param>
/// <param name="Quantity">The quantity of units of the item purchased in this detail.</param>
public record PurchaseDetailResource(
    int Id,
    int SupplyPurchaseId,
    int ItemTypeId,
    float UnitPrice,
    int Quantity
);