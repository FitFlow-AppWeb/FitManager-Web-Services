using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Inventory.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IItemBookingRepository"/> interface, providing concrete data access operations for <see cref="ItemBooking"/> aggregates.
    /// This repository interacts with the database using Entity Framework Core and resides in the Infrastructure layer,
    /// encapsulating data persistence details.
    /// </summary>
    public class ItemBookingRepository : IItemBookingRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBookingRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public ItemBookingRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all item bookings from the database.
        /// </summary>
        /// <remarks>
        /// This method eagerly loads the associated <see cref="Item"/> and <see cref="Employee"/>
        /// entities to ensure complete booking details are available.
        /// </remarks>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="ItemBooking"/> objects.
        /// </returns>
        public async Task<IEnumerable<ItemBooking>> GetAllAsync()
        {
            return await _context.ItemBookings
                .Include(ib => ib.Item)
                //.Include(ib => ib.Employee)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an <see cref="ItemBooking"/> by its unique identifier.
        /// </summary>
        /// <remarks>
        /// Includes the related <see cref="Item"/> and <see cref="Employee"/> data.
        /// </remarks>
        /// <param name="id">The unique identifier of the booking.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains the <see cref="ItemBooking"/> if found; otherwise, null.
        /// </returns>
        public async Task<ItemBooking?> GetByIdAsync(int id)
        {
            return await _context.ItemBookings
                .Include(ib => ib.Item)
                //.Include(ib => ib.Employee)
                .FirstOrDefaultAsync(ib => ib.Id == id);
        }

        /// <summary>
        /// Asynchronously adds a new item booking to the database.
        /// </summary>
        /// <param name="booking">The <see cref="ItemBooking"/> to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddAsync(ItemBooking booking)
        {
            await _context.ItemBookings.AddAsync(booking);
        }

        /// <summary>
        /// Updates an existing item booking in the database.
        /// </summary>
        /// <param name="booking">The <see cref="ItemBooking"/> to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateAsync(ItemBooking booking)
        {
            _context.ItemBookings.Update(booking);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously deletes an item booking by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the booking to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var booking = await GetByIdAsync(id);
            if (booking != null)
            {
                _context.ItemBookings.Remove(booking);
            }
        }
    }
}
