using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

public class MembershipPaymentRepository : BaseRepository<MembershipPayment>, IMembershipPaymentRepository
{
    public MembershipPaymentRepository(AppDbContext context) : base(context) { }
    
    public async Task<IEnumerable<MembershipPayment>> GetByMonthYearAsync(int month, int year)
    {
        return await _context.MembershipPayments
            .Where(p => p.Date.Month == month && p.Date.Year == year)
            .ToListAsync();
    }
}