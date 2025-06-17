using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Specialty>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Specialties
                .Where(s => ids.Contains(s.Id))
                .ToListAsync();
        }


    }
}