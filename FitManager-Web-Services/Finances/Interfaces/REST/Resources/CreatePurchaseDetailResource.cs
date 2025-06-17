namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

public record CreatePurchaseDetailResource(
    int SupplyPurchaseId,
    int ItemTypeId,
    float UnitPrice,
    int Quantity
);