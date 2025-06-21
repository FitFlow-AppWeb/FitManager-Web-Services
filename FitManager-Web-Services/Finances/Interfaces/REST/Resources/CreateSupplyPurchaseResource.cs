using System.ComponentModel.DataAnnotations;
namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new supply purchase, used for data transfer objects (DTOs) in RESTful API requests.
    /// </summary>
    /// <param name="Date">The date when the supply purchase was made. Required.</param>
    /// <param name="Amount">The total amount of the supply purchase. Required.</param>
    /// <param name="Method">The payment method used for the purchase (e.g., "Credit Card", "Bank Transfer"). Required.</param>
    /// <param name="Currency">The currency of the supply purchase (e.g., "USD", "PEN"). Required.</param>
    /// <param name="VendorName">The name of the vendor or supplier from whom the purchase was made. Required.</param>
    /// <param name="PurchaseDetails">An optional list of <see cref="CreatePurchaseDetailResource"/> representing the individual items or services included in this purchase.</param>
    public record CreateSupplyPurchaseResource(
        [Required] DateTime Date,
        [Required] float Amount,
        [Required] string Method,
        [Required] string Currency,
        [Required] string VendorName,
        List<CreatePurchaseDetailResource>? PurchaseDetails
    );
}