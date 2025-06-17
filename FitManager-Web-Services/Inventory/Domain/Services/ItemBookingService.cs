using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Services
{
    /// <summary>
    /// Provides a concrete implementation of the <see cref="IItemBookingService"/> interface.
    /// This domain service manages operations related to <see cref="ItemBooking"/> entities,
    /// coordinating with the repository and ensuring transactional consistency via the Unit of Work pattern.
    /// </summary>
    public class ItemBookingService : IItemBookingService
    {
        private readonly IItemBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBookingService"/> class.
        /// </summary>
        /// <param name="bookingRepository">The repository used for accessing item booking data.</param>
        /// <param name="unitOfWork">The unit of work for managing transactional operations.</param>
        public ItemBookingService(IItemBookingRepository bookingRepository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Asynchronously retrieves all item bookings.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="ItemBooking"/> objects.</returns>
        public Task<IEnumerable<ItemBooking>> GetAllAsync()
        {
            return _bookingRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an item booking by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item booking.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains the <see cref="ItemBooking"/> object if found; otherwise, null.</returns>
        public Task<ItemBooking> GetByIdAsync(int id)
        {
            return _bookingRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Asynchronously adds a new item booking and commits the transaction.
        /// </summary>
        /// <param name="booking">The <see cref="ItemBooking"/> object to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddAsync(ItemBooking booking)
        {
            await _bookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing item booking and commits the transaction.
        /// </summary>
        /// <param name="booking">The <see cref="ItemBooking"/> object to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateAsync(ItemBooking booking)
        {
            await _bookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously deletes an item booking by its unique identifier and commits the transaction.
        /// </summary>
        /// <param name="id">The unique identifier of the item booking to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            await _bookingRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
