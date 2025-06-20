using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Inventory.Domain.Repositories; // Puede que no necesites este aquí si solo es para ItemType
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks; // Para Task

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices
{
    public class PurchaseDetailCommandService
    {
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;
        private readonly ISupplyPurchaseRepository _supplyPurchaseRepository; // Usado para GetByIdAsync
        private readonly IItemTypeRepository _itemTypeRepository; // Usado para GetByIdAsync
        private readonly IUnitOfWork _unitOfWork;

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