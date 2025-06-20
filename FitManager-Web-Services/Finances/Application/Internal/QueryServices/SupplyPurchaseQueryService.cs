using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving supply purchase information.
    /// This service handles queries related to purchases of supplies and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class SupplyPurchaseQueryService
    {
        private readonly ISupplyPurchaseRepository _supplyPurchaseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyPurchaseQueryService"/> class.
        /// </summary>
        /// <param name="supplyPurchaseRepository">The supply purchase repository.</param>
        public SupplyPurchaseQueryService(ISupplyPurchaseRepository supplyPurchaseRepository)
        {
            _supplyPurchaseRepository = supplyPurchaseRepository;
        }

        /// <summary>
        /// Retrieves all supply purchase records.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all <see cref="SupplyPurchase"/> aggregates to the <see cref="ISupplyPurchaseRepository"/>.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of all <see cref="SupplyPurchase"/> objects.</returns>
        public async Task<IEnumerable<SupplyPurchase>> GetAllAsync()
        {
            return await _supplyPurchaseRepository.GetAllAsync();
        }
    }
}