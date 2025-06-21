using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IBookingService"/> contract, providing concrete business logic
    /// for managing <see cref="Booking"/> records.
    /// </summary>
    /// <remarks>
    /// This service acts as a mediator for booking-related operations, handling the creation,
    /// cancellation, and retrieval of class bookings. It interacts with <see cref="IBookingRepository"/>
    /// for data persistence.
    /// </remarks>
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="bookingRepository">The repository for <see cref="Booking"/> entities.</param>
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        /// <summary>
        /// Asynchronously creates a new class booking.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member making the booking.</param>
        /// <param name="classId">The unique identifier of the class being booked.</param>
        /// <param name="date">The specific date and time for which the class is being booked.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the newly created <see cref="Booking"/> object.
        /// </returns>
        public async Task<Booking> CreateBookingAsync(int memberId, int classId, DateTime date)
        {
            var booking = new Booking(memberId, classId, date);
            await _bookingRepository.AddAsync(booking);
            return booking;
        }

        /// <summary>
        /// Asynchronously cancels an existing booking by its unique identifier.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to cancel.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the booking with the specified ID is not found.</exception>
        public async Task CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }
            
            await _bookingRepository.DeleteAsync(booking);
        }

        /// <summary>
        /// Asynchronously retrieves all booking records for a specific member.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Booking"/> objects
        /// associated with the specified member.
        /// </returns>
        public async Task<IEnumerable<Booking>> GetBookingsByMemberAsync(int memberId) => 
            await _bookingRepository.FindByMemberAsync(memberId);

        /// <summary>
        /// Asynchronously retrieves all booking records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Booking"/> objects
        /// associated with the specified class.
        /// </returns>
        public async Task<IEnumerable<Booking>> GetBookingsByClassAsync(int classId) => 
            await _bookingRepository.FindByClassAsync(classId);
        
        /// <summary>
        /// Asynchronously retrieves a single booking record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the booking record.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Booking"/> if found, otherwise <c>null</c>.
        /// </returns>
        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }
    }
}