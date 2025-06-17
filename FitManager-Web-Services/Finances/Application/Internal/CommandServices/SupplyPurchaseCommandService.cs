using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices;

public class SupplyPurchaseCommandService
{
    private readonly ISupplyPurchaseRepository _supplyPurchaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SupplyPurchaseCommandService(
        ISupplyPurchaseRepository supplyPurchaseRepository,
        IUnitOfWork unitOfWork)
    {
        _supplyPurchaseRepository = supplyPurchaseRepository;
        _unitOfWork = unitOfWork;
    }

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