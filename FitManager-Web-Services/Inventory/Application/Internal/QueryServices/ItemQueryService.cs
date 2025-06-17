using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Queries;
using FitManager_Web_Services.Inventory.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving item information.
    /// This service handles queries related to inventory items and interacts with the item repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for read-only operations.
    /// </summary>
    public class ItemQueryService
    {
        private readonly IItemRepository _itemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemQueryService"/> class.
        /// </summary>
        /// <param name="itemRepository">The repository for accessing items.</param>
        public ItemQueryService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all items.
        /// </summary>
        /// <param name="query">The query object for retrieving all items.</param>
        /// <returns>An asynchronous operation returning a collection of <see cref="Item"/>.</returns>
        public async Task<IEnumerable<Item>> Handle(GetAllItemsQuery query)
        {
            return await _itemRepository.GetAllAsync();
        }
    }
}
