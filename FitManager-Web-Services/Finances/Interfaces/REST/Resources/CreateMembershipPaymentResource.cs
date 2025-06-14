namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

public record CreateMembershipPaymentResource(
    DateTime Date,
    float Amount,
    string Method, 
    string Currency, 
    int MemberId
);