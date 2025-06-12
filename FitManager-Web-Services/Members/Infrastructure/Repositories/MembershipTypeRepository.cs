using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories; 
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; 

namespace FitManager_Web_Services.Members.Infrastructure.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly AppDbContext _context;

        public MembershipTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembershipType>> GetAllAsync()
        {
            return await _context.MembershipTypes.ToListAsync();
        }

        public async Task<MembershipType?> GetByIdAsync(int id)
        {
            return await _context.MembershipTypes.FirstOrDefaultAsync(mt => mt.Id == id);
        }
    }
}