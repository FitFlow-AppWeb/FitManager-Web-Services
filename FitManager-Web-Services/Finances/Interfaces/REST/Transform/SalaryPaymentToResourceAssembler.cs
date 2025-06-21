using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="SalaryPaymentResource"/> DTOs from <see cref="SalaryPayment"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// </remarks>
public static class SalaryPaymentToResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="SalaryPayment"/> entity to a <see cref="SalaryPaymentResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="SalaryPayment"/> entity to convert.</param>
    /// <returns>A new <see cref="SalaryPaymentResource"/> representing the converted entity.</returns>
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

    /// <summary>
    /// Converts a collection of <see cref="SalaryPayment"/> entities to an enumerable of <see cref="SalaryPaymentResource"/> DTOs.
    /// </summary>
    /// <param name="entities">The collection of <see cref="SalaryPayment"/> entities to convert.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="SalaryPaymentResource"/> representing the converted entities.</returns>
    public static IEnumerable<SalaryPaymentResource> ToResourceListFromEntityList(IEnumerable<SalaryPayment> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}