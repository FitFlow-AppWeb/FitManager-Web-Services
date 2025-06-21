using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="PurchaseDetail"/> entities.
    /// </summary>
    /// <remarks>
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations
    /// for <see cref="PurchaseDetail"/> entities. It also includes a specific method to retrieve
    /// purchase details associated with a particular supply purchase.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface IPurchaseDetailRepository : IBaseRepository<PurchaseDetail>
    {
        /// <summary>
        /// Asynchronously retrieves a collection of purchase details for a specified supply purchase.
        /// </summary>
        /// <param name="purchaseId">The unique identifier of the supply purchase for which to retrieve details.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="PurchaseDetail"/> objects
        /// associated with the given <paramref name="purchaseId"/>.
        /// </returns>
        Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int purchaseId);
    }
}