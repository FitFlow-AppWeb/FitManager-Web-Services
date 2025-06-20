using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Finances.Infrastructure.Repositories;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

using System;
using System.Threading.Tasks; 

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing membership payments.
    /// This service orchestrates business logic related to creating membership payment records.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling finance-related use cases.
    /// </summary>
    public class MembershipPaymentCommandService
    {
        private readonly IMembershipPaymentRepository _membershipPaymentRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipPaymentCommandService"/> class.
        /// </summary>
        /// <param name="membershipPaymentRepository">The membership payment repository for managing payment data.</param>
        /// <param name="memberRepository">The member repository for accessing member data to validate member existence.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
        public MembershipPaymentCommandService(
            IMembershipPaymentRepository membershipPaymentRepository,
            IMemberRepository memberRepository,
            IUnitOfWork unitOfWork)
        {
            _membershipPaymentRepository = membershipPaymentRepository;
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new membership payment record.
        /// </summary>
        /// <remarks>
        /// This method first validates the existence of the member associated with the payment.
        /// If the member exists, it creates a new <see cref="MembershipPayment"/> aggregate
        /// with the provided details, persists it to the database, and completes the unit of work.
        /// </remarks>
        /// <param name="date">The date of the payment.</param>
        /// <param name="amount">The amount of the payment.</param>
        /// <param name="method">The method used for the payment (e.g., "Cash", "Card").</param>
        /// <param name="currency">The currency of the payment (e.g., "USD", "EUR").</param>
        /// <param name="memberId">The unique identifier of the member making the payment.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="MembershipPayment"/> aggregate if successful, otherwise null
        /// if the specified member is not found.</returns>
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
}