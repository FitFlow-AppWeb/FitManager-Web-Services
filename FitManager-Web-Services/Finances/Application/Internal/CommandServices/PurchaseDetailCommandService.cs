using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing purchase details.
    /// This service orchestrates business logic related to creating individual line items within supply purchases.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling finance-related use cases
    /// concerning the specifics of a purchase.
    /// </summary>
    public class PurchaseDetailCommandService
    {
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;
        private readonly ISupplyPurchaseRepository _supplyPurchaseRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDetailCommandService"/> class.
        /// </summary>
        /// <param name="purchaseDetailRepository">The purchase detail repository for managing purchase detail data.</param>
        /// <param name="supplyPurchaseRepository">The supply purchase repository for accessing supply purchase data to validate existence.</param>
        /// <param name="itemTypeRepository">The item type repository for accessing item type data to validate existence.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
        public PurchaseDetailCommandService(
            IPurchaseDetailRepository purchaseDetailRepository,
            ISupplyPurchaseRepository supplyPurchaseRepository,
            IItemTypeRepository itemTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _purchaseDetailRepository = purchaseDetailRepository;
            _supplyPurchaseRepository = supplyPurchaseRepository;
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new purchase detail record.
        /// </summary>
        /// <remarks>
        /// This method validates the existence of the associated supply purchase and item type.
        /// If both exist, it creates a new <see cref="PurchaseDetail"/> aggregate
        /// with the provided details, persists it to the database, and completes the unit of work.
        /// You might choose to remove these validations here if your controller already performs them,
        /// or keep them for robustness within the command service itself.
        /// </remarks>
        /// <param name="supplyPurchaseId">The unique identifier of the supply purchase to which this detail belongs.</param>
        /// <param name="itemTypeId">The unique identifier of the item type being purchased.</param>
        /// <param name="unitPrice">The price per unit of the item.</param>
        /// <param name="quantity">The quantity of the item purchased.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="PurchaseDetail"/> aggregate if successful, otherwise null
        /// if the specified supply purchase or item type is not found.</returns>
        public async Task<PurchaseDetail?> CreateAsync(int supplyPurchaseId, int itemTypeId, float unitPrice, int quantity)
        {
            var supplyPurchase = await _supplyPurchaseRepository.GetByIdAsync(supplyPurchaseId);
            var itemType = await _itemTypeRepository.GetByIdAsync(itemTypeId);

            if (supplyPurchase == null || itemType == null)
                return null; 

            var detail = new PurchaseDetail(supplyPurchaseId, itemTypeId, unitPrice, quantity);
            await _purchaseDetailRepository.AddAsync(detail);
            await _unitOfWork.CompleteAsync();

            return detail;
        }
    }
}