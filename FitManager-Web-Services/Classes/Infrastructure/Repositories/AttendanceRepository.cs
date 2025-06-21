using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="IAttendanceRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository provides persistence operations for <see cref="Attendance"/> entities.
/// It interacts directly with the <see cref="AppDbContext"/> and includes eager loading
/// for related <see cref="Attendance.Member"/> and <see cref="Attendance.Class"/> entities
/// where appropriate.
/// </remarks>
public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttendanceRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public AttendanceRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves an attendance record by its unique identifier,
    /// including its associated member and class details.
    /// </summary>
    /// <param name="id">The unique identifier of the attendance record.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="Attendance"/> if found (with related data included),
    /// otherwise <c>null</c>.
    /// </returns>
    public async Task<Attendance?> GetByIdAsync(int id)
    {
        return await _context.Attendances
            .Include(a => a.Member)
            .Include(a => a.Class)  
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Asynchronously retrieves all attendance records for a specific class,
    /// including associated member details.
    /// </summary>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Attendance"/> objects
    /// associated with the specified <paramref name="classId"/> (with member details included).
    /// </returns>
    public async Task<IEnumerable<Attendance>> FindByClassAsync(int classId)
    {
        return await _context.Attendances
            .Where(a => a.ClassId == classId)
            .Include(a => a.Member)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all attendance records for a specific member,
    /// including associated class details.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Attendance"/> objects
    /// associated with the specified <paramref name="memberId"/> (with class details included).
    /// </returns>
    public async Task<IEnumerable<Attendance>> FindByMemberAsync(int memberId)
    {
        return await _context.Attendances
            .Where(a => a.MemberId == memberId)
            .Include(a => a.Class)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously adds a new attendance record to the repository.
    /// </summary>
    /// <param name="attendance">The <see cref="Attendance"/> entity to add.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task AddAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously updates an existing attendance record in the repository.
    /// </summary>
    /// <param name="attendance">The <see cref="Attendance"/> entity with updated information.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes an attendance record from the repository.
    /// </summary>
    /// <param name="attendance">The <see cref="Attendance"/> entity to delete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(Attendance attendance)
    {
        _context.Attendances.Remove(attendance);
        await _context.SaveChangesAsync();
    }
}