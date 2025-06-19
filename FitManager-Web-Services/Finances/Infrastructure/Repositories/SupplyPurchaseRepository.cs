using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

public class SupplyPurchaseRepository : BaseRepository<SupplyPurchase>, ISupplyPurchaseRepository
{
    public SupplyPurchaseRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<SupplyPurchase>> GetAllAsync()
    {
        return await _context.SupplyPurchases
            .Include(sp => sp.PurchaseDetails)
            .ToListAsync();
    }

    public override async Task<SupplyPurchase?> GetByIdAsync(int id)
    {
        return await _context.SupplyPurchases
            .Where(sp => sp.Id == id)
            .Include(sp => sp.PurchaseDetails)
            .FirstOrDefaultAsync(); 
    }

    public async Task<IEnumerable<SupplyPurchase>> GetByMonthYearAsync(int month, int year)
    {
        return await _context.SupplyPurchases
            .Where(sp => sp.Date.Month == month && sp.Date.Year == year)
            .Include(sp => sp.PurchaseDetails)
            .ToListAsync();
    }
}