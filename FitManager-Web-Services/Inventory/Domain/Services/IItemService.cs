using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Inventory.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for item-related operations.
    /// In Domain-Driven Design (DDD), domain services encapsulate domain logic that doesn't
    /// naturally fit within a single aggregate or coordinates actions between multiple aggregates.
    /// This interface provides a facade for managing item entities.
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Asynchronously retrieves all items.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// a collection of <see cref="Item"/> objects.</returns>
        Task<IEnumerable<Item>> GetAllAsync();

        /// <summary>
        /// Asynchronously adds a new item to the inventory.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Item item);

        /// <summary>
        /// Asynchronously updates an existing item.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Item item);

        /// <summary>
        /// Asynchronously deletes an item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(int id);
    }
}