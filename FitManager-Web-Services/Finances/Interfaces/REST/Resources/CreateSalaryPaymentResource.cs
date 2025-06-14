namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

public record CreateSalaryPaymentResource(
    DateTime Date,
    float Amount,
    string Method,
    string Currency,
    int EmployeeId
);