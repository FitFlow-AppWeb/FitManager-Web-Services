namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for a salary payment, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the salary payment.</param>
/// <param name="Date">The date when the salary payment was made.</param>
/// <param name="Amount">The amount of the salary payment.</param>
/// <param name="Method">The payment method used for the salary (e.g., "Bank Transfer", "Cash").</param>
/// <param name="Currency">The currency of the salary payment (e.g., "USD", "PEN").</param>
/// <param name="EmployeeId">The unique identifier of the employee who received the salary payment.</param>
public record SalaryPaymentResource(
    int Id,
    DateTime Date,
    float Amount,
    string Method,
    string Currency,
    int EmployeeId
);