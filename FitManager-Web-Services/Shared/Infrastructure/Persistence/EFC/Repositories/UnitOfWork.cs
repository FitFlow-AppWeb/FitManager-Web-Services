// Shared/Infrastructure/Persistence/EFC/Repositories/UnitOfWork.cs

using FitManager_Web_Services.Shared.Domain.Repositories; 
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; 

namespace FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Implements the <see cref="IUnitOfWork"/> interface, providing a concrete Unit of Work implementation using Entity Framework Core.
/// This class is responsible for committing all changes tracked by the <see cref="AppDbContext"/> to the database,
/// ensuring transactional consistency for a set of operations. It resides in the Infrastructure layer.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously commits all pending changes (adds, updates, deletes) tracked by the DbContext
    /// to the underlying database. This method ensures that all operations within the current
    /// Unit of Work are saved as a single atomic transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous save operation.</returns>
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}