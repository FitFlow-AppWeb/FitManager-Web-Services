using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for managing <see cref="Attendance"/> records.
    /// </summary>
    /// <remarks>
    /// This interface provides a facade for attendance-related business operations,
    /// including registering new attendance, updating existing records, and retrieving attendance
    /// based on various criteria.
    /// </remarks>
    public interface IAttendanceService
    {
        /// <summary>
        /// Asynchronously registers a new attendance record for a member in a class.
        /// </summary>
        /// <param name="entryTime">The precise date and time when the member entered the class.</param>
        /// <param name="exitTime">The precise date and time when the member exited the class.</param>
        /// <param name="memberId">The unique identifier of the member whose attendance is being registered.</param>
        /// <param name="classId">The unique identifier of the class for which the attendance is being recorded.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the newly created <see cref="Attendance"/> object.
        /// </returns>
        Task<Attendance> RegisterAttendanceAsync(DateTime entryTime, DateTime exitTime, int memberId, int classId);

        /// <summary>
        /// Asynchronously updates the entry and exit times of an existing attendance record.
        /// </summary>
        /// <param name="id">The unique identifier of the attendance record to update.</param>
        /// <param name="entryTime">The updated entry time for the attendance record.</param>
        /// <param name="exitTime">The updated exit time for the attendance record.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAttendanceAsync(int id, DateTime entryTime, DateTime exitTime);

        /// <summary>
        /// Asynchronously retrieves all attendance records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Attendance"/> objects
        /// associated with the specified class.
        /// </returns>
        Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId);

        /// <summary>
        /// Asynchronously retrieves all attendance records for a specific member.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Attendance"/> objects
        /// associated with the specified member.
        /// </returns>
        Task<IEnumerable<Attendance>> GetAttendancesByMemberAsync(int memberId);

        /// <summary>
        /// Asynchronously retrieves a single attendance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the attendance record.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Attendance"/> if found, otherwise <c>null</c>.
        /// </returns>
        Task<Attendance?> GetAttendanceByIdAsync(int id);
    }
}