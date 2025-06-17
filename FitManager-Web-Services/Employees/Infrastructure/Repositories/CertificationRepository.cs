using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    public class CertificationRepository : BaseRepository<Certification>, ICertificationRepository
    {
        public CertificationRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Certification>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Certifications
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }


    }
    
}