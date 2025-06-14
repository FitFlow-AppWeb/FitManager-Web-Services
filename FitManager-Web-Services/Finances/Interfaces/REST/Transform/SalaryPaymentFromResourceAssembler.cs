using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class SalaryPaymentFromResourceAssembler
{
    public static SalaryPayment ToEntityFromResource(CreateSalaryPaymentResource resource)
    {
        return new SalaryPayment(
            resource.Date,
            resource.Amount,
            resource.Method,
            resource.Currency,
            resource.EmployeeId
        );
    }
}