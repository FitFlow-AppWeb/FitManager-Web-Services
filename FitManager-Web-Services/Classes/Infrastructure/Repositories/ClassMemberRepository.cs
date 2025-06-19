using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

public class ClassMemberRepository : IClassMemberRepository
{
    private readonly AppDbContext _context;

    public ClassMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ClassMember classMember)
    {
        await _context.ClassMembers.AddAsync(classMember);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int memberId, int classId)
    {
        var classMember = await _context.ClassMembers
            .FirstOrDefaultAsync(cm => cm.MemberId == memberId && cm.ClassId == classId);
        
        if (classMember != null)
        {
            _context.ClassMembers.Remove(classMember);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int memberId, int classId)
    {
        return await _context.ClassMembers
            .AnyAsync(cm => cm.MemberId == memberId && cm.ClassId == classId);
    }

    public async Task<ClassMember?> FindByIdsAsync(int memberId, int classId)
    {
        return await _context.ClassMembers
            .Include(cm => cm.Member)
            .Include(cm => cm.Class)
            .FirstOrDefaultAsync(cm => cm.MemberId == memberId && cm.ClassId == classId);
    }
}