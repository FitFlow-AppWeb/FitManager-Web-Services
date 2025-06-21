using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Inventory.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for item booking-related operations.
    /// In Domain-Driven Design (DDD), domain services are used to encapsulate domain logic
    /// that doesn't naturally belong to an entity or value object.
    /// This interface provides CRUD operations for managing <see cref="ItemBooking"/> aggregates.
    /// </summary>
    public interface IItemBookingService
    {
        /// <summary>
        /// Asynchronously retrieves all item bookings.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="ItemBooking"/> objects.</returns>
        Task<IEnumerable<ItemBooking>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves an item booking by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item booking.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains the <see cref="ItemBooking"/> object if found; otherwise, null.</returns>
        Task<ItemBooking> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously adds a new item booking.
        /// </summary>
        /// <param name="booking">The <see cref="ItemBooking"/> object to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(ItemBooking booking);

        /// <summary>
        /// Asynchronously updates an existing item booking.
        /// </summary>
        /// <param name="booking">The <see cref="ItemBooking"/> object to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(ItemBooking booking);

        /// <summary>
        /// Asynchronously deletes an item booking by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item booking to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(int id);
    }
}
