using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices;

public class MembershipPaymentQueryService
{
    private readonly IMembershipPaymentRepository _membershipPaymentRepository;

    public MembershipPaymentQueryService(IMembershipPaymentRepository membershipPaymentRepository)
    {
        _membershipPaymentRepository = membershipPaymentRepository;
    }
    
    public async Task<IEnumerable<MembershipPayment>> GetAllAsync()
    {
        return await _membershipPaymentRepository.GetAllAsync();
    }
}