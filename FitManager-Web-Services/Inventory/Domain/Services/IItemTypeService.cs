using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Inventory.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for item type-related operations.
    /// In Domain-Driven Design (DDD), domain services encapsulate domain logic that does not naturally belong to a specific entity or value object.
    /// This interface provides a read-only facade for accessing item type data.
    /// </summary>
    public interface IItemTypeService
    {
        /// <summary>
        /// Asynchronously retrieves all item types.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// an enumerable collection of <see cref="ItemType"/> objects.</returns>
        Task<IEnumerable<ItemType>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves an item type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item type.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// the <see cref="ItemType"/> if found; otherwise, null.</returns>
        Task<ItemType> GetByIdAsync(int id);
    }
}