using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing inventory items.
    /// This service orchestrates business logic related to creating, updating, and deleting items,
    /// including their relationship with <see cref="ItemType"/> and employee assignments.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling inventory-specific use cases.
    /// </summary>
    public class ItemCommandService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCommandService"/> class.
        /// </summary>
        /// <param name="itemRepository">The item repository for managing item data.</param>
        /// <param name="itemTypeRepository">The item type repository for accessing item type data to validate existence.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
        public ItemCommandService(
            IItemRepository itemRepository,
            IItemTypeRepository itemTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a new item based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method validates the existence of the associated item type.
        /// If the item type exists, it creates a new <see cref="Item"/> aggregate
        /// with the provided details, persists it to the database, and completes the unit of work.
        /// </remarks>
        /// <param name="command">The command containing the data for the new item.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="Item"/> aggregate if successful, otherwise null
        /// if the specified item type is not found.</returns>
        public async Task<Item?> Handle(CreateItemCommand command)
        {
            var itemType = await _itemTypeRepository.GetByIdAsync(command.ItemTypeId);
            if (itemType == null) return null;

            var item = new Item(
                command.LastMaintenanceDate,
                command.NextMaintenanceDate,
                command.Status,
                command.ItemTypeId,
                command.EmployeeId
            );

            await _itemRepository.AddAsync(item);
            await _unitOfWork.CompleteAsync();

            return item;
        }

        /// <summary>
        /// Handles the update of an existing item based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method retrieves an existing <see cref="Item"/> aggregate,
        /// validates the new item type if changed, applies updates to its maintenance information,
        /// changes its associated item type, assigns a new employee if provided,
        /// and then persists all changes to the database.
        /// </remarks>
        /// <param name="command">The command containing the data for the item update.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the updated <see cref="Item"/> aggregate if found and updated, otherwise null
        /// if the existing item or the new item type is not found.</returns>
        public async Task<Item?> Handle(UpdateItemCommand command)
        {
            var existingItem = await _itemRepository.GetByIdAsync(command.Id);
            if (existingItem == null) return null;

            var newItemType = await _itemTypeRepository.GetByIdAsync(command.ItemTypeId);
            if (newItemType == null) return null;

            existingItem.UpdateMaintenance(
                command.LastMaintenanceDate,
                command.NextMaintenanceDate,
                command.Status
            );

            existingItem.ChangeItemType(command.ItemTypeId);

            if (command.EmployeeId.HasValue)
            {
                existingItem.AssignEmployee(command.EmployeeId.Value);
            }

            await _itemRepository.UpdateAsync(existingItem);
            await _unitOfWork.CompleteAsync();

            return existingItem;
        }

        /// <summary>
        /// Handles the deletion of an existing item based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method first retrieves the <see cref="Item"/> aggregate by its ID
        /// to ensure it exists, then deletes it using the item repository,
        /// and finally completes the unit of work.
        /// </remarks>
        /// <param name="command">The command containing the ID of the item to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// is true if the item was deleted successfully, false otherwise (e.g., if not found).</returns>
        public async Task<bool> Handle(DeleteItemCommand command)
        {
            var item = await _itemRepository.GetByIdAsync(command.Id);
            if (item == null) return false;

            await _itemRepository.DeleteAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}