using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="Booking"/> entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for common data access operations related to class bookings.
    /// It includes specific queries to find bookings by member or by class, ensuring the domain layer
    /// can interact with persistence in an abstract manner.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface IBookingRepository
    {
        /// <summary>
        /// Asynchronously retrieves a single booking record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the booking record.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Booking"/> if found, otherwise null.
        /// </returns>
        Task<Booking?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a collection of booking records for a specific member.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member for whom to retrieve booking records.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Booking"/> objects
        /// associated with the given <paramref name="memberId"/>.
        /// </returns>
        Task<IEnumerable<Booking>> FindByMemberAsync(int memberId);

        /// <summary>
        /// Asynchronously retrieves a collection of booking records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class for which to retrieve booking records.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Booking"/> objects
        /// associated with the given <paramref name="classId"/>.
        /// </returns>
        Task<IEnumerable<Booking>> FindByClassAsync(int classId);

        /// <summary>
        /// Asynchronously adds a new booking record to the repository.
        /// </summary>
        /// <param name="booking">The <see cref="Booking"/> entity to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task AddAsync(Booking booking);

        /// <summary>
        /// Asynchronously updates an existing booking record in the repository.
        /// </summary>
        /// <param name="booking">The <see cref="Booking"/> entity with updated information.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAsync(Booking booking);

        /// <summary>
        /// Asynchronously deletes a booking record from the repository.
        /// </summary>
        /// <param name="booking">The <see cref="Booking"/> entity to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteAsync(Booking booking);
    }
}