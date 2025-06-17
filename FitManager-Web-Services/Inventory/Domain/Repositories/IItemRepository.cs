using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="Item"/> aggregates.
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide standard CRUD operations,
    /// and includes additional methods relevant to the <see cref="Item"/> entity within the inventory context.
    /// </summary>
    public interface IItemRepository : IBaseRepository<Item>
    {
        /// <summary>
        /// Asynchronously retrieves an <see cref="Item"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Item"/> if found; otherwise, null.
        /// </returns>
        Task<Item?> GetByIdAsync(int id);
    }
}