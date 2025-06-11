using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Members.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)           
                .ThenInclude(ms => ms.MembershipType)   
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)           
                .ThenInclude(ms => ms.MembershipType)   
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Member?> GetByDniAsync(int dni)
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)           
                .ThenInclude(ms => ms.MembershipType)  
            .FirstOrDefaultAsync(m => m.Dni == dni);
    }

    public async Task AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
    }

    public async Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
    }

    public async Task DeleteAsync(int id)
    {

        var member = await GetByIdAsync(id); 
        if (member != null)
        {
            _context.Members.Remove(member);
        }
    }
}