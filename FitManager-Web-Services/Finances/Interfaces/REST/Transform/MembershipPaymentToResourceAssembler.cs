using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="MembershipPaymentResource"/> DTOs from <see cref="MembershipPayment"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// </remarks>
public static class MembershipPaymentToResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="MembershipPayment"/> entity to a <see cref="MembershipPaymentResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="MembershipPayment"/> entity to convert.</param>
    /// <returns>A new <see cref="MembershipPaymentResource"/> representing the converted entity.</returns>
    public static MembershipPaymentResource ToResourceFromEntity(MembershipPayment entity)
    {
        return new MembershipPaymentResource(
            entity.Id,
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.MemberId
        );
    }

    /// <summary>
    /// Converts a collection of <see cref="MembershipPayment"/> entities to an enumerable of <see cref="MembershipPaymentResource"/> DTOs.
    /// </summary>
    /// <param name="entities">The collection of <see cref="MembershipPayment"/> entities to convert.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="MembershipPaymentResource"/> representing the converted entities.</returns>
    public static IEnumerable<MembershipPaymentResource> ToResourceListFromEntityList(IEnumerable<MembershipPayment> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}