using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices;

public class SupplyPurchaseQueryService
{
    private readonly ISupplyPurchaseRepository _supplyPurchaseRepository;

    public SupplyPurchaseQueryService(ISupplyPurchaseRepository supplyPurchaseRepository)
    {
        _supplyPurchaseRepository = supplyPurchaseRepository;
    }

    public async Task<IEnumerable<SupplyPurchase>> GetAllAsync()
    {
        return await _supplyPurchaseRepository.GetAllAsync();
    }
}