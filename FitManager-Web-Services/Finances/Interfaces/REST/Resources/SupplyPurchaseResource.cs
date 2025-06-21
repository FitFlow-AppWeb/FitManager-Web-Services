namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for a supply purchase, used for data transfer objects (DTOs) in RESTful APIs.
    /// </summary>
    /// <param name="Id">The unique identifier of the supply purchase.</param>
    /// <param name="Date">The date when the supply purchase was made.</param>
    /// <param name="Amount">The total amount of the supply purchase.</param>
    /// <param name="Method">The payment method used for the purchase (e.g., "Credit Card", "Bank Transfer").</param>
    /// <param name="Currency">The currency of the supply purchase (e.g., "USD", "PEN").</param>
    /// <param name="VendorName">The name of the vendor or supplier from whom the purchase was made.</param>
    /// <param name="PurchaseDetails">An optional collection of <see cref="PurchaseDetailResource"/> representing the individual items or services included in this purchase.</param>
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