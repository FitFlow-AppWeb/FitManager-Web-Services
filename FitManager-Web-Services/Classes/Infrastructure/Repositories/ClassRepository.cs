using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="IClassRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository provides persistence operations for <see cref="Class"/> aggregates.
/// It interacts directly with the <see cref="AppDbContext"/> and includes eager loading
/// for related entities such as <see cref="Class.Employee"/>, <see cref="Class.ClassMembers"/>,
/// <see cref="Class.Bookings"/>, and <see cref="Class.Attendances"/> where appropriate,
/// to support comprehensive data retrieval for classes.
/// </remarks>
public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public ClassRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves a single class by its unique identifier,
    /// including its associated members, bookings, and attendance records.
    /// </summary>
    /// <param name="id">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="Class"/> if found (with related data included),
    /// otherwise <c>null</c>.
    /// </returns>
    public async Task<Class?> GetByIdAsync(int id)
    {
        return await _context.Classes
            .Include(c => c.ClassMembers)  
                .ThenInclude(cm => cm.Member) 
            .Include(c => c.Bookings)      
            .Include(c => c.Attendances)     
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Asynchronously retrieves all fitness classes, including their associated employee (instructor)
    /// and a list of members enrolled in each class.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of all <see cref="Class"/> objects,
    /// with their <see cref="Class.Employee"/> and <see cref="Class.ClassMembers"/> (including associated members) included.
    /// </returns>
    public async Task<IEnumerable<Class>> ListAsync()
    {
        return await _context.Classes
            .Include(c => c.Employee)      
            .Include(c => c.ClassMembers)   
                .ThenInclude(cm => cm.Member)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a collection of classes filtered by their type,
    /// including their associated employee (instructor) and members.
    /// </summary>
    /// <param name="type">The type or category of the classes to retrieve (e.g., "Yoga", "Cardio").</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Class"/> objects
    /// matching the specified <paramref name="type"/> (with employee and member details included).
    /// </returns>
    public async Task<IEnumerable<Class>> FindByTypeAsync(string type)
    {
        return await _context.Classes
            .Where(c => c.Type == type)
            .Include(c => c.Employee)    
            .Include(c => c.ClassMembers)   
                .ThenInclude(cm => cm.Member) 
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously adds a new fitness class record to the repository.
    /// </summary>
    /// <param name="fitnessClass">The <see cref="Class"/> entity to add.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task AddAsync(Class fitnessClass)
    {
        await _context.Classes.AddAsync(fitnessClass);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously updates an existing fitness class record in the repository.
    /// </summary>
    /// <param name="fitnessClass">The <see cref="Class"/> entity with updated information.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(Class fitnessClass)
    {
        _context.Classes.Update(fitnessClass);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes a fitness class record from the repository.
    /// </summary>
    /// <param name="fitnessClass">The <see cref="Class"/> entity to delete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(Class fitnessClass)
    {
        _context.Classes.Remove(fitnessClass);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all <see cref="ClassMember"/> associations for a specific class,
    /// including the associated member details for each.
    /// </summary>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="ClassMember"/> objects
    /// associated with the specified <paramref name="classId"/> (with member details included).
    /// </returns>
    public async Task<IEnumerable<ClassMember>> GetMembersByClassIdAsync(int classId)
    {
        return await _context.ClassMembers
            .Where(cm => cm.ClassId == classId)
            .Include(cm => cm.Member)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all booking records for a specific class,
    /// including the associated member details for each booking.
    /// </summary>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Booking"/> objects
    /// associated with the specified <paramref name="classId"/> (with member details included).
    /// </returns>
    public async Task<IEnumerable<Booking>> GetBookingsByClassIdAsync(int classId)
    {
        return await _context.Bookings
            .Where(b => b.ClassId == classId)
            .Include(b => b.Member)
            .ToListAsync();
    }
}