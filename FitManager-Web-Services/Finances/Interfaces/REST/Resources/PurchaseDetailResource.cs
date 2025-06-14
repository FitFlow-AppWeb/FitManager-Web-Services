namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

public record PurchaseDetailResource(
    int Id,
    int SupplyPurchaseId,
    int ItemTypeId,
    float UnitPrice,
    int Quantity
);