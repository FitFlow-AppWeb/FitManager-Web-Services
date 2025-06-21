using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for managing <see cref="Booking"/> records.
    /// </summary>
    /// <remarks>
    /// This interface provides a facade for booking-related business operations,
    /// including creating and canceling bookings, and retrieving bookings
    /// based on members or classes.
    /// </remarks>
    public interface IBookingService
    {
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
        Task<Booking> CreateBookingAsync(int memberId, int classId, DateTime date);

        /// <summary>
        /// Asynchronously cancels an existing booking by its unique identifier.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to cancel.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task CancelBookingAsync(int bookingId);

        /// <summary>
        /// Asynchronously retrieves all booking records for a specific member.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Booking"/> objects
        /// associated with the specified member.
        /// </returns>
        Task<IEnumerable<Booking>> GetBookingsByMemberAsync(int memberId);

        /// <summary>
        /// Asynchronously retrieves all booking records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Booking"/> objects
        /// associated with the specified class.
        /// </returns>
        Task<IEnumerable<Booking>> GetBookingsByClassAsync(int classId);
        
        /// <summary>
        /// Asynchronously retrieves a single booking record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the booking record.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Booking"/> if found, otherwise <c>null</c>.
        /// </returns>
        Task<Booking?> GetBookingByIdAsync(int id);
    }
}