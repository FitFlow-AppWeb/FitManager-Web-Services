using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class SalaryPaymentToResourceAssembler
{
    public static SalaryPaymentResource ToResourceFromEntity(SalaryPayment entity)
    {
        return new SalaryPaymentResource(
            entity.Id,
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.EmployeeId
        );
    }

    public static IEnumerable<SalaryPaymentResource> ToResourceListFromEntityList(IEnumerable<SalaryPayment> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}