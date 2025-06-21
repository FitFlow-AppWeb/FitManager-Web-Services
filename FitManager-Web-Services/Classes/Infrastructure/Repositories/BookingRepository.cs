using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="IBookingRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository provides persistence operations for <see cref="Booking"/> entities.
/// It interacts directly with the <see cref="AppDbContext"/> and includes eager loading
/// for related <see cref="Booking.Member"/> and <see cref="Booking.Class"/> entities
/// where appropriate.
/// </remarks>
public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves a booking record by its unique identifier,
    /// including its associated member and class details.
    /// </summary>
    /// <param name="id">The unique identifier of the booking record.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="Booking"/> if found (with related data included),
    /// otherwise <c>null</c>.
    /// </returns>
    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .Include(b => b.Member)
            .Include(b => b.Class)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    /// <summary>
    /// Asynchronously retrieves all booking records for a specific member,
    /// including associated class details.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Booking"/> objects
    /// associated with the specified <paramref name="memberId"/> (with class details included).
    /// </returns>
    public async Task<IEnumerable<Booking>> FindByMemberAsync(int memberId)
    {
        return await _context.Bookings
            .Where(b => b.MemberId == memberId)
            .Include(b => b.Class)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all booking records for a specific class,
    /// including associated member details.
    /// </summary>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Booking"/> objects
    /// associated with the specified <paramref name="classId"/> (with member details included).
    /// </returns>
    public async Task<IEnumerable<Booking>> FindByClassAsync(int classId)
    {
        return await _context.Bookings
            .Where(b => b.ClassId == classId)
            .Include(b => b.Member)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously adds a new booking record to the repository.
    /// </summary>
    /// <param name="booking">The <see cref="Booking"/> entity to add.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously updates an existing booking record in the repository.
    /// </summary>
    /// <param name="booking">The <see cref="Booking"/> entity with updated information.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes a booking record from the repository.
    /// </summary>
    /// <param name="booking">The <see cref="Booking"/> entity to delete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(Booking booking)
    {
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
    }
}