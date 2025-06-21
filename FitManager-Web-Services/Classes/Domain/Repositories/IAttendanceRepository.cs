using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="Attendance"/> entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for common data access operations related to class attendance records.
    /// It includes specific queries to find attendance by class or by member, ensuring the domain layer
    /// can interact with persistence in an abstract manner.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface IAttendanceRepository
    {
        /// <summary>
        /// Asynchronously retrieves a single attendance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the attendance record.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Attendance"/> if found, otherwise null.
        /// </returns>
        Task<Attendance?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a collection of attendance records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class for which to retrieve attendance records.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Attendance"/> objects
        /// associated with the given <paramref name="classId"/>.
        /// </returns>
        Task<IEnumerable<Attendance>> FindByClassAsync(int classId);

        /// <summary>
        /// Asynchronously retrieves a collection of attendance records for a specific member.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member for whom to retrieve attendance records.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Attendance"/> objects
        /// associated with the given <paramref name="memberId"/>.
        /// </returns>
        Task<IEnumerable<Attendance>> FindByMemberAsync(int memberId);

        /// <summary>
        /// Asynchronously adds a new attendance record to the repository.
        /// </summary>
        /// <param name="attendance">The <see cref="Attendance"/> entity to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task AddAsync(Attendance attendance);

        /// <summary>
        /// Asynchronously updates an existing attendance record in the repository.
        /// </summary>
        /// <param name="attendance">The <see cref="Attendance"/> entity with updated information.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAsync(Attendance attendance);

        /// <summary>
        /// Asynchronously deletes an attendance record from the repository.
        /// </summary>
        /// <param name="attendance">The <see cref="Attendance"/> entity to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteAsync(Attendance attendance);
    }
}