namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for creating a new membership payment, used for data transfer objects (DTOs) in RESTful API requests.
/// </summary>
/// <param name="Date">The date when the payment was made.</param>
/// <param name="Amount">The amount of the payment.</param>
/// <param name="Method">The payment method used (e.g., "Credit Card", "Cash", "Bank Transfer").</param>
/// <param name="Currency">The currency of the payment (e.g., "USD", "EUR", "PEN").</param>
/// <param name="MemberId">The unique identifier of the member making the payment.</param>
public record CreateMembershipPaymentResource(
    DateTime Date,
    float Amount,
    string Method, 
    string Currency, 
    int MemberId
);