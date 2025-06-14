using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

public class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailRepository
{
    public PurchaseDetailRepository(AppDbContext context) : base(context) { }
    
    
    public async Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int purchaseId)
    {
        return await _context.PurchaseDetails
            .Where(pd => pd.SupplyPurchaseId == purchaseId)
            .ToListAsync();
    }
}