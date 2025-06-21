using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="SalaryPayment"/> entities from <see cref="CreateSalaryPaymentResource"/> DTOs.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's entities.
/// It translates the incoming <see cref="CreateSalaryPaymentResource"/> (from an HTTP request) into a
/// <see cref="SalaryPayment"/> entity that can be used by the domain layer.
/// </remarks>
public static class SalaryPaymentFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateSalaryPaymentResource"/> DTO to a <see cref="SalaryPayment"/> entity.
    /// </summary>
    /// <param name="resource">The <see cref="CreateSalaryPaymentResource"/> to convert.</param>
    /// <returns>A new <see cref="SalaryPayment"/> entity representing the converted resource.</returns>
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