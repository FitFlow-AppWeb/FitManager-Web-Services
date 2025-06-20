using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

using System;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing supply purchases.
    /// This service orchestrates business logic related to creating new records for purchases of supplies.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling finance-related use cases
    /// concerning the acquisition of goods.
    /// </summary>
    public class SupplyPurchaseCommandService
    {
        private readonly ISupplyPurchaseRepository _supplyPurchaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyPurchaseCommandService"/> class.
        /// </summary>
        /// <param name="supplyPurchaseRepository">The supply purchase repository for managing purchase data.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
        public SupplyPurchaseCommandService(
            ISupplyPurchaseRepository supplyPurchaseRepository,
            IUnitOfWork unitOfWork)
        {
            _supplyPurchaseRepository = supplyPurchaseRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new supply purchase record.
        /// </summary>
        /// <remarks>
        /// This method instantiates a new <see cref="SupplyPurchase"/> aggregate with the provided details,
        /// persists it to the database using the supply purchase repository, and completes the unit of work.
        /// </remarks>
        /// <param name="date">The date when the supply purchase was made.</param>
        /// <param name="amount">The total amount of the supply purchase.</param>
        /// <param name="method">The payment method used for the purchase (e.g., "Cash", "Credit Card").</param>
        /// <param name="currency">The currency of the purchase amount (e.g., "USD", "PEN").</param>
        /// <param name="vendorName">The name of the vendor from whom the supplies were purchased.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="SupplyPurchase"/> aggregate.</returns>
        public async Task<SupplyPurchase> CreateAsync(
            DateTime date, float amount, string method, string currency, string vendorName
        )
        {
            var purchase = new SupplyPurchase(date, amount, method, currency, vendorName);
            await _supplyPurchaseRepository.AddAsync(purchase);
            await _unitOfWork.CompleteAsync();

            return purchase;
        }
    }
}