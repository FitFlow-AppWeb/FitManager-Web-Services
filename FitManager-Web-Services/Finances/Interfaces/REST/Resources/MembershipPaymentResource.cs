namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for a membership payment, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the membership payment.</param>
/// <param name="Date">The date when the payment was made.</param>
/// <param name="Amount">The amount of the payment.</param>
/// <param name="Method">The payment method used (e.g., "Credit Card", "Cash", "Bank Transfer").</param>
/// <param name="Currency">The currency of the payment (e.g., "USD", "EUR", "PEN").</param>
/// <param name="MemberId">The unique identifier of the member who made the payment.</param>
public record MembershipPaymentResource(
    int Id,
    DateTime Date,
    float Amount,
    string Method,
    string Currency,
    int MemberId
);