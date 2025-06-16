using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="ItemType"/> aggregates.
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations,
    /// and includes specific queries related to item types.
    /// </summary>
    public interface IItemTypeRepository : IBaseRepository<ItemType>
    {
        /// <summary>
        /// Asynchronously retrieves an <see cref="ItemType"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the item type.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, with the result containing
        /// the matching <see cref="ItemType"/> or null if not found.
        /// </returns>
        Task<ItemType?> GetByIdAsync(int id);
    }
}