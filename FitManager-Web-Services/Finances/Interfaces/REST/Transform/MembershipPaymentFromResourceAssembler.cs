using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="MembershipPayment"/> entities from <see cref="CreateMembershipPaymentResource"/> DTOs.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's entities.
/// It translates the incoming <see cref="CreateMembershipPaymentResource"/> (from an HTTP request) into a
/// <see cref="MembershipPayment"/> entity that can be used by the domain layer.
/// </remarks>
public static class MembershipPaymentFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateMembershipPaymentResource"/> DTO to a <see cref="MembershipPayment"/> entity.
    /// </summary>
    /// <param name="resource">The <see cref="CreateMembershipPaymentResource"/> to convert.</param>
    /// <returns>A new <see cref="MembershipPayment"/> entity representing the converted resource.</returns>
    public static MembershipPayment ToEntityFromResource(CreateMembershipPaymentResource resource)
    {
        return new MembershipPayment(
            resource.Date,
            resource.Amount,
            resource.Method,
            resource.Currency,
            resource.MemberId
        );
    }
}