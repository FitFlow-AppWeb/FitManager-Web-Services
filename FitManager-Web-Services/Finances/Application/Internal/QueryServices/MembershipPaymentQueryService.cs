using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving membership payment information.
    /// This service handles queries related to membership payments and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class MembershipPaymentQueryService
    {
        private readonly IMembershipPaymentRepository _membershipPaymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipPaymentQueryService"/> class.
        /// </summary>
        /// <param name="membershipPaymentRepository">The membership payment repository.</param>
        public MembershipPaymentQueryService(IMembershipPaymentRepository membershipPaymentRepository)
        {
            _membershipPaymentRepository = membershipPaymentRepository;
        }
        
        /// <summary>
        /// Retrieves all membership payment records.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all <see cref="MembershipPayment"/> aggregates to the <see cref="IMembershipPaymentRepository"/>.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of all <see cref="MembershipPayment"/> objects.</returns>
        public async Task<IEnumerable<MembershipPayment>> GetAllAsync()
        {
            return await _membershipPaymentRepository.GetAllAsync();
        }
    }
}