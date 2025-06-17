using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Inventory.Domain.Services;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IItemService"/> interface, providing concrete implementations
    /// for item-related domain operations.
    /// While called a "Domain Service," this class primarily acts as a façade over the <see cref="IItemRepository"/>
    /// and coordinates the <see cref="IUnitOfWork"/> for transaction management.
    /// In more complex scenarios, this service could orchestrate logic across multiple aggregates.
    /// </summary>
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemService"/> class.
        /// </summary>
        /// <param name="itemRepository">The repository used to access item data.</param>
        /// <param name="unitOfWork">The unit of work to manage transactional consistency.</param>
        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Asynchronously retrieves all items from the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The task result contains an enumerable collection of <see cref="Item"/> objects.</returns>
        public Task<IEnumerable<Item>> GetAllAsync()
        {
            return _itemRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously adds a new item to the repository and commits the transaction.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddAsync(Item item)
        {
            await _itemRepository.AddAsync(item);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing item in the repository and commits the transaction.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Item item)
        {
            await _itemRepository.UpdateAsync(item);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously deletes an item by its unique identifier from the repository and commits the transaction.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            await _itemRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
