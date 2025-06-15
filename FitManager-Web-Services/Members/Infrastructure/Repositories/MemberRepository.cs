// Members/Infrastructure/Repositories/MemberRepository.cs
using Microsoft.EntityFrameworkCore; 
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; 
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories; 

namespace FitManager_Web_Services.Members.Infrastructure.Repositories;

/// <summary>
/// Implements the <see cref="IMemberRepository"/> interface, providing concrete data access operations for <see cref="Member"/> aggregates.
/// This repository leverages Entity Framework Core and extends <see cref="BaseRepository{TEntity}"/> for common CRUD functionalities.
/// It resides in the Infrastructure layer, handling the details of data persistence.
/// </summary>
public class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (AppDbContext).</param>
    public MemberRepository(AppDbContext context) : base(context) 
    {
    }

    /// <summary>
    /// Asynchronously retrieves a <see cref="Member"/> by their DNI (National Identity Document).
    /// </summary>
    /// <remarks>
    /// This method includes the associated <see cref="MembershipStatus"/> and its <see cref="MembershipType"/>
    /// to eagerly load related data, which is often required by the domain logic or presentation layer.
    /// </remarks>
    /// <param name="dni">The DNI of the member to retrieve.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="Member"/> if found, otherwise null.
    /// </returns>
    public async Task<Member?> GetByDniAsync(int dni)
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)
                .ThenInclude(ms => ms.MembershipType)
            .FirstOrDefaultAsync(m => m.Dni == dni);
    }

    /// <summary>
    /// Overrides the base method to asynchronously retrieve all members.
    /// </summary>
    /// <remarks>
    /// This method eagerly loads the associated <see cref="MembershipStatus"/> and its <see cref="MembershipType"/>
    /// for each member, ensuring related data is available when accessed.
    /// </remarks>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an enumerable collection of <see cref="Member"/> objects.
    /// </returns>
    public override async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)
                .ThenInclude(ms => ms.MembershipType)
            .ToListAsync();
    }

    /// <summary>
    /// Overrides the base method to asynchronously retrieve a <see cref="Member"/> by their unique identifier.
    /// </summary>
    /// <remarks>
    /// This method eagerly loads the associated <see cref="MembershipStatus"/> and its <see cref="MembershipType"/>
    /// to ensure complete member details are retrieved.
    /// </remarks>
    /// <param name="id">The unique identifier of the member to retrieve.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="Member"/> if found, otherwise null.
    /// </returns>
    public override async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members
            .Include(m => m.MembershipStatus)
                .ThenInclude(ms => ms.MembershipType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}