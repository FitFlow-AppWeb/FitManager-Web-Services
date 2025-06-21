using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="IPurchaseDetailRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository extends <see cref="BaseRepository{TEntity}"/> to inherit common CRUD operations
/// and implements the specific queries defined in <see cref="IPurchaseDetailRepository"/>
/// for <see cref="PurchaseDetail"/> entities. It interacts directly with the database context.
/// </remarks>
public class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PurchaseDetailRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public PurchaseDetailRepository(AppDbContext context) : base(context) { }
    
    /// <summary>
    /// Asynchronously retrieves a collection of purchase details associated with a specific supply purchase.
    /// </summary>
    /// <param name="purchaseId">The unique identifier of the supply purchase.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="PurchaseDetail"/> objects
    /// linked to the specified <paramref name="purchaseId"/>.
    /// </returns>
    public async Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int purchaseId)
    {
        return await _context.PurchaseDetails
            .Where(pd => pd.SupplyPurchaseId == purchaseId)
            .ToListAsync();
    }
}