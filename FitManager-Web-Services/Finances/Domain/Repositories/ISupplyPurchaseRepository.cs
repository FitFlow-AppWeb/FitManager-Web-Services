using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="SupplyPurchase"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations
    /// for <see cref="SupplyPurchase"/> entities. It also includes specific methods to query
    /// supply purchases based on a given financial period (month and year).
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface ISupplyPurchaseRepository : IBaseRepository<SupplyPurchase>
    {
        /// <summary>
        /// Asynchronously retrieves a collection of supply purchases for a specific month and year.
        /// </summary>
        /// <param name="month">The month (1-12) for which to retrieve purchases.</param>
        /// <param name="year">The year for which to retrieve purchases.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="SupplyPurchase"/> objects
        /// matching the specified month and year.
        /// </returns>
        Task<IEnumerable<SupplyPurchase>> GetByMonthYearAsync(int month, int year);
    }
}