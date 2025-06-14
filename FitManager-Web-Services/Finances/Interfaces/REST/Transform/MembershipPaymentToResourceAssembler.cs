using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class MembershipPaymentToResourceAssembler
{
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

    public static IEnumerable<MembershipPaymentResource> ToResourceListFromEntityList(IEnumerable<MembershipPayment> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}