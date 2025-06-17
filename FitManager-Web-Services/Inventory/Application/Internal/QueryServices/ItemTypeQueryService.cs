using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Queries;
using FitManager_Web_Services.Inventory.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving item type information.
    /// This service handles queries related to item types and interacts with the item type repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for read-only operations.
    /// </summary>
    public class ItemTypeQueryService
    {
        private readonly IItemTypeRepository _itemTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTypeQueryService"/> class.
        /// </summary>
        /// <param name="itemTypeRepository">The repository for accessing item types.</param>
        public ItemTypeQueryService(IItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all item types.
        /// </summary>
        /// <param name="query">The query object for retrieving all item types.</param>
        /// <returns>An asynchronous operation returning a collection of <see cref="ItemType"/>.</returns>
        public async Task<IEnumerable<ItemType>> Handle(GetAllItemTypesQuery query)
        {
            return await _itemTypeRepository.GetAllAsync();
        }
    }
}