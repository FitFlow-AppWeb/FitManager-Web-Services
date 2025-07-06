using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories; // Assuming IUnitOfWork is here

namespace FitManager_Web_Services.Inventory.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing ItemType entities.
    /// This service orchestrates business logic related to creating item types.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling ItemType-specific use cases.
    /// </summary>
    public class ItemTypeCommandService
    {
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTypeCommandService"/> class.
        /// </summary>
        /// <param name="itemTypeRepository">The item type repository for managing item type data.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
        public ItemTypeCommandService(
            IItemTypeRepository itemTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a new item type based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method creates a new <see cref="ItemType"/> aggregate with the provided details,
        /// persists it to the database, and completes the unit of work.
        /// </remarks>
        /// <param name="command">The command containing the data for the new item type.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="ItemType"/> aggregate if successful, otherwise null
        /// if, for example, an item type with the same name already exists (if uniqueness constraint is applied).</returns>
        public async Task<ItemType?> Handle(CreateItemTypeCommand command)
        {
            // Optional: Add validation logic here, e.g., check for duplicate name
            // var existingItemType = await _itemTypeRepository.FindByNameAsync(command.Name); // Requires a custom method in IItemTypeRepository
            // if (existingItemType != null) return null; // Or throw a specific exception

            var itemType = new ItemType(
                command.Name,
                command.Description
            );

            await _itemTypeRepository.AddAsync(itemType);
            await _unitOfWork.CompleteAsync();

            return itemType;
        }

        // If you need update/delete for ItemType, you would add similar Handle methods here.
    }
}