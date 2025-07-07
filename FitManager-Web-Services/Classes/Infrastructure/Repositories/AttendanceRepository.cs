using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using System; // Necesario para DateTime
using System.Collections.Generic; // Necesario para IEnumerable
using System.Threading.Tasks;

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

    // ... (Métodos GetByIdAsync, FindByClassAsync, FindByMemberAsync, y GetAllAsync sin cambios, están bien) ...

    public async Task<Attendance?> GetByIdAsync(int id)
    {
        return await _context.Attendances
            .Include(a => a.Member)
            .Include(a => a.Class)  
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Attendance>> FindByClassAsync(int classId)
    {
        return await _context.Attendances
            .Where(a => a.ClassId == classId)
            .Include(a => a.Member)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> FindByMemberAsync(int memberId)
    {
        return await _context.Attendances
            .Where(a => a.MemberId == memberId)
            .Include(a => a.Class)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> GetAllAsync()
    {
        return await _context.Attendances
            .Include(a => a.Member) 
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
    }

    /// <summary>
    /// Asynchronously updates an existing attendance record in the repository.
    /// </summary>
    /// <param name="attendance">The <see cref="Attendance"/> entity with updated information.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
    }

    /// <summary>
    /// Asynchronously deletes an attendance record from the repository.
    /// </summary>
    /// <param name="attendance">The <see cref="Attendance"/> entity to delete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(Attendance attendance)
    {
        _context.Attendances.Remove(attendance);
    }
    
    /// <summary>
    /// Asynchronously retrieves an attendance record for a specific member, class, and date.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <param name="attendanceDate">The specific date to check for attendance (time part will be ignored).</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="Attendance"/> if found, otherwise null.</returns>
    public async Task<Attendance?> GetByMemberClassAndDateAsync(int memberId, int classId, DateTime attendanceDate)
    {
        // Importante: Usamos .Date para comparar solo la parte de la fecha, ignorando la hora
        return await _context.Attendances
            .FirstOrDefaultAsync(a => a.MemberId == memberId &&
                                     a.ClassId == classId &&
                                     a.EntryTime.Date == attendanceDate.Date);
    }
}