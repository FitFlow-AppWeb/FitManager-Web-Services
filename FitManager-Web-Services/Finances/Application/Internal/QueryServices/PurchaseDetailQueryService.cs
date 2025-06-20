using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

using System.Collections.Generic; 
using System.Threading.Tasks; 

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving purchase detail information.
    /// This service handles queries related to individual line items within supply purchases and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class PurchaseDetailQueryService
    {
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDetailQueryService"/> class.
        /// </summary>
        /// <param name="purchaseDetailRepository">The purchase detail repository.</param>
        public PurchaseDetailQueryService(IPurchaseDetailRepository purchaseDetailRepository)
        {
            _purchaseDetailRepository = purchaseDetailRepository;
        }

        /// <summary>
        /// Retrieves all purchase detail records.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all <see cref="PurchaseDetail"/> aggregates to the <see cref="IPurchaseDetailRepository"/>.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of all <see cref="PurchaseDetail"/> objects.</returns>
        public async Task<IEnumerable<PurchaseDetail>> GetAllAsync()
        {
            return await _purchaseDetailRepository.GetAllAsync();
        }

        /// <summary>
        /// Retrieves all purchase detail records associated with a specific supply purchase.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of <see cref="PurchaseDetail"/> aggregates filtered by a supply purchase ID
        /// to the <see cref="IPurchaseDetailRepository"/>.
        /// </remarks>
        /// <param name="supplyPurchaseId">The unique identifier of the supply purchase whose details are to be retrieved.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="PurchaseDetail"/> objects associated with the specified supply purchase.</returns>
        public async Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int supplyPurchaseId)
        {
            return await _purchaseDetailRepository.GetByPurchaseIdAsync(supplyPurchaseId);
        }
    }
}