using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

public class SupplyPurchaseRepository : BaseRepository<SupplyPurchase>, ISupplyPurchaseRepository
{
    public SupplyPurchaseRepository(AppDbContext context) : base(context) { }
    
    public async Task<IEnumerable<SupplyPurchase>> GetByMonthYearAsync(int month, int year)
    {
        return await _context.SupplyPurchases
            .Where(sp => sp.Date.Month == month && sp.Date.Year == year)
            .ToListAsync();
    }
}