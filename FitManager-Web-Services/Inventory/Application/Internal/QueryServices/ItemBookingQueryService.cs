using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Queries;
using FitManager_Web_Services.Inventory.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving item booking information.
    /// This service handles queries related to item bookings and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class ItemBookingQueryService
    {
        private readonly IItemBookingRepository _itemBookingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBookingQueryService"/> class.
        /// </summary>
        /// <param name="itemBookingRepository">The item booking repository.</param>
        public ItemBookingQueryService(IItemBookingRepository itemBookingRepository)
        {
            _itemBookingRepository = itemBookingRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all item bookings.
        /// </summary>
        /// <param name="query">The query object for retrieving all item bookings.</param>
        /// <returns>An asynchronous operation that returns an enumerable collection of <see cref="ItemBooking"/> objects.</returns>
        public async Task<IEnumerable<ItemBooking>> Handle(GetAllItemBookingsQuery query)
        {
            return await _itemBookingRepository.GetAllAsync();
        }
    }
}