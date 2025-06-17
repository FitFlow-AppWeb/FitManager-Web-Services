using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Domain.Repositories;

public interface IPurchaseDetailRepository : IBaseRepository<PurchaseDetail>
{
    Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int purchaseId);
}