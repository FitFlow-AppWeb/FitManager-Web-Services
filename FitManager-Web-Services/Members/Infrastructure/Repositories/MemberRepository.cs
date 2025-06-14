using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories; 
using System.Collections.Generic; 

namespace FitManager_Web_Services.Members.Infrastructure.Repositories;

public class MemberRepository : BaseRepository<Member>, IMemberRepository
{


    public MemberRepository(AppDbContext context) : base(context) 
    {
    }

    public async Task<Member?> GetByDniAsync(int dni)
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)
                .ThenInclude(ms => ms.MembershipType)
            .FirstOrDefaultAsync(m => m.Dni == dni);
    }

  
    public override async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)
                .ThenInclude(ms => ms.MembershipType)
            .ToListAsync();
    }

    public override async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)
                .ThenInclude(ms => ms.MembershipType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

   
}