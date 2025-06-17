using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices;

public class PurchaseDetailQueryService
{
    private readonly IPurchaseDetailRepository _purchaseDetailRepository;

    public PurchaseDetailQueryService(IPurchaseDetailRepository purchaseDetailRepository)
    {
        _purchaseDetailRepository = purchaseDetailRepository;
    }

    public async Task<IEnumerable<PurchaseDetail>> GetAllAsync()
    {
        return await _purchaseDetailRepository.GetAllAsync();
    }

    public async Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int supplyPurchaseId)
    {
        return await _purchaseDetailRepository.GetByPurchaseIdAsync(supplyPurchaseId);
    }
}