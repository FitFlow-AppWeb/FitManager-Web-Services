using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class MembershipPaymentFromResourceAssembler
{
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