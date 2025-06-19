using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Application.Internal.CommandServices
{
    /// <summary>
    /// Command service responsible for handling the business logic to create, update, and delete items,
    /// including their relationship with ItemType.
    /// </summary>
    public class ItemCommandService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

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
        /// Creates a new item, validating the existence of its associated type.
        /// </summary>
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
        /// Updates an existing item, allowing changes to its maintenance info and item type.
        /// </summary>
        public async Task<Item?> Handle(UpdateItemCommand command)
        {
            var existingItem = await _itemRepository.GetByIdAsync(command.Id);
            if (existingItem == null) return null;

            // Validate new item type
            var newItemType = await _itemTypeRepository.GetByIdAsync(command.ItemTypeId);
            if (newItemType == null) return null;

            // Apply domain-level updates
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
        /// Deletes an item by its ID.
        /// </summary>
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
