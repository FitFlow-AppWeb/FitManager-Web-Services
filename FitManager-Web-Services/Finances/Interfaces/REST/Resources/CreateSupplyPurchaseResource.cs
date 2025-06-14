namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

public record CreateSupplyPurchaseResource(
    DateTime Date,
    float Amount,
    string Method,
    string Currency,
    string VendorName
);