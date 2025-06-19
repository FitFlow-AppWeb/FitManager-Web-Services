using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _context;

    public ClassRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Class?> GetByIdAsync(int id)
    {
        return await _context.Classes
            .Include(c => c.ClassMembers)
            .Include(c => c.Bookings)
            .Include(c => c.Attendances)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Class>> ListAsync()
    {
        return await _context.Classes
            .Include(c => c.Employee)
            .ToListAsync();
    }

    public async Task<IEnumerable<Class>> FindByTypeAsync(string type)
    {
        return await _context.Classes
            .Where(c => c.Type == type)
            .Include(c => c.Employee)
            .ToListAsync();
    }

    public async Task AddAsync(Class fitnessClass)
    {
        await _context.Classes.AddAsync(fitnessClass);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Class fitnessClass)
    {
        _context.Classes.Update(fitnessClass);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Class fitnessClass)
    {
        _context.Classes.Remove(fitnessClass);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ClassMember>> GetMembersByClassIdAsync(int classId)
    {
        return await _context.ClassMembers
            .Where(cm => cm.ClassId == classId)
            .Include(cm => cm.Member)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingsByClassIdAsync(int classId)
    {
        return await _context.Bookings
            .Where(b => b.ClassId == classId)
            .Include(b => b.Member)
            .ToListAsync();
    }
}