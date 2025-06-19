namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources
{
    public record SupplyPurchaseResource(
        int Id,
        DateTime Date,
        float Amount,
        string Method,
        string Currency,
        string VendorName,
        IEnumerable<PurchaseDetailResource>? PurchaseDetails
    );
}