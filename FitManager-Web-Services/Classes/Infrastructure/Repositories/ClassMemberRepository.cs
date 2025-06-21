using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; 
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Classes.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="IClassMemberRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository manages the persistence of <see cref="ClassMember"/> entities,
/// which represent the many-to-many relationship between members and classes.
/// It provides methods for adding, removing, checking existence, and finding specific
/// class-member associations, including eager loading for related <see cref="ClassMember.Member"/>
/// and <see cref="ClassMember.Class"/> entities.
/// </remarks>
public class ClassMemberRepository : IClassMemberRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassMemberRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public ClassMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously adds a new class-member association to the repository.
    /// </summary>
    /// <param name="classMember">The <see cref="ClassMember"/> entity to add.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task AddAsync(ClassMember classMember)
    {
        await _context.ClassMembers.AddAsync(classMember);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously removes a class-member association based on the member and class IDs.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
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

    /// <summary>
    /// Asynchronously checks if a specific class-member association already exists.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains <c>true</c> if the association exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> ExistsAsync(int memberId, int classId)
    {
        return await _context.ClassMembers
            .AnyAsync(cm => cm.MemberId == memberId && cm.ClassId == classId);
    }

    /// <summary>
    /// Asynchronously retrieves a class-member association by its composite key (member ID and class ID),
    /// including associated member and class details.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="ClassMember"/> if found (with related data included),
    /// otherwise <c>null</c>.
    /// </returns>
    public async Task<ClassMember?> FindByIdsAsync(int memberId, int classId)
    {
        return await _context.ClassMembers
            .Include(cm => cm.Member)
            .Include(cm => cm.Class)
            .FirstOrDefaultAsync(cm => cm.MemberId == memberId && cm.ClassId == classId);
    }
}