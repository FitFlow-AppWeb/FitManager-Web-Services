using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Finances.Infrastructure.Repositories;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices;

public class MembershipPaymentCommandService
{
    private readonly IMembershipPaymentRepository _membershipPaymentRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MembershipPaymentCommandService(
        IMembershipPaymentRepository membershipPaymentRepository,
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _membershipPaymentRepository = membershipPaymentRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MembershipPayment?> CreateAsync(
        DateTime date, float amount, string method, string currency, int memberId
        )
    {
        var member = await _memberRepository.GetByIdAsync(memberId);
        if (member == null) return null;
        
        var payment = new MembershipPayment(date, amount, method, currency, memberId);
        await _membershipPaymentRepository.AddAsync(payment);
        await _unitOfWork.CompleteAsync();
        
        return payment;
    }
}