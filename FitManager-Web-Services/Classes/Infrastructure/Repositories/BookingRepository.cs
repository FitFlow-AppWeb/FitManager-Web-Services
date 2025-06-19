using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .Include(b => b.Member)
            .Include(b => b.Class)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Booking>> FindByMemberAsync(int memberId)
    {
        return await _context.Bookings
            .Where(b => b.MemberId == memberId)
            .Include(b => b.Class)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> FindByClassAsync(int classId)
    {
        return await _context.Bookings
            .Where(b => b.ClassId == classId)
            .Include(b => b.Member)
            .ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Booking booking)
    {
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
    }
}