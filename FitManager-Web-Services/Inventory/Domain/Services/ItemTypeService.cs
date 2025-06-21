using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Services
{
    /// <summary>
    /// Provides a concrete implementation of the <see cref="IItemTypeService"/> interface.
    /// This service is responsible for handling read-only operations related to item types.
    /// It acts as a façade over the <see cref="IItemTypeRepository"/> and follows
    /// the Domain Service pattern in Domain-Driven Design (DDD).
    /// </summary>
    public class ItemTypeService : IItemTypeService
    {
        private readonly IItemTypeRepository _itemTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTypeService"/> class.
        /// </summary>
        /// <param name="itemTypeRepository">The repository used to access item type data.</param>
        public ItemTypeService(IItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }

        /// <summary>
        /// Asynchronously retrieves all item types from the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="ItemType"/> objects.</returns>
        public Task<IEnumerable<ItemType>> GetAllAsync()
        {
            return _itemTypeRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an item type by its unique identifier from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the item type.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains the <see cref="ItemType"/> object if found; otherwise, null.</returns>
        public Task<ItemType> GetByIdAsync(int id)
        {
            return _itemTypeRepository.GetByIdAsync(id);
        }
    }
}
