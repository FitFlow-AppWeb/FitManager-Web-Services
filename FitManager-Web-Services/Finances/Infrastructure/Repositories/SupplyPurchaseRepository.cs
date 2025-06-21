using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="ISupplyPurchaseRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository extends <see cref="BaseRepository{TEntity}"/> to inherit common CRUD operations
/// and implements the specific queries defined in <see cref="ISupplyPurchaseRepository"/>
/// for <see cref="SupplyPurchase"/> aggregates. It ensures that <see cref="PurchaseDetail"/>
/// entities are eagerly loaded when retrieving supply purchases.
/// </remarks>
public class SupplyPurchaseRepository : BaseRepository<SupplyPurchase>, ISupplyPurchaseRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SupplyPurchaseRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public SupplyPurchaseRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Asynchronously retrieves all supply purchase records, including their associated purchase details.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of all <see cref="SupplyPurchase"/> objects,
    /// with their <see cref="SupplyPurchase.PurchaseDetails"/> included.
    /// </returns>
    public override async Task<IEnumerable<SupplyPurchase>> GetAllAsync()
    {
        return await _context.SupplyPurchases
            .Include(sp => sp.PurchaseDetails)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a single supply purchase by its unique identifier, including its associated purchase details.
    /// </summary>
    /// <param name="id">The unique identifier of the supply purchase.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <see cref="SupplyPurchase"/> if found (with its <see cref="SupplyPurchase.PurchaseDetails"/> included),
    /// otherwise <c>null</c>.
    /// </returns>
    public override async Task<SupplyPurchase?> GetByIdAsync(int id)
    {
        return await _context.SupplyPurchases
            .Where(sp => sp.Id == id)
            .Include(sp => sp.PurchaseDetails)
            .FirstOrDefaultAsync(); 
    }

    /// <summary>
    /// Asynchronously retrieves a collection of supply purchases for a specific month and year, including their associated purchase details.
    /// </summary>
    /// <param name="month">The month (1-12) for which to retrieve purchases.</param>
    /// <param name="year">The year for which to retrieve purchases.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="SupplyPurchase"/> objects
    /// matching the specified month and year, with their <see cref="SupplyPurchase.PurchaseDetails"/> included.
    /// </returns>
    public async Task<IEnumerable<SupplyPurchase>> GetByMonthYearAsync(int month, int year)
    {
        return await _context.SupplyPurchases
            .Where(sp => sp.Date.Month == month && sp.Date.Year == year)
            .Include(sp => sp.PurchaseDetails)
            .ToListAsync();
    }
}