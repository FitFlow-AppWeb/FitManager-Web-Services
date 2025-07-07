using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System; // Necesario para DateTime
using System.Collections.Generic; // Necesario para IEnumerable
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Defines the contract for a domain service that provides business logic
    /// for querying <see cref="Attendance"/> records.
    /// </summary>
    /// <remarks>
    /// This interface focuses on providing query capabilities and potentially
    /// more complex business rules related to attendance that don't fit
    /// directly within the repository or the aggregate root.
    /// Operations that modify state (e.g., Register, Update) are intentionally
    /// excluded from this service, as they should be handled by Command Services/Handlers.
    /// </remarks>
    public interface IAttendanceService
    {
        /// <summary>
        /// Asynchronously retrieves a collection of attendance records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Attendance"/> objects
        /// associated with the specified class.
        /// </returns>
        Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId);

        /// <summary>
        /// Asynchronously retrieves a collection of attendance records for a specific member.
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
        
        /// <summary>
        /// Asynchronously retrieves all attendance records from the system.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of all <see cref="Attendance"/> objects.
        /// </returns>
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync();

        /// <summary>
        /// Asynchronously checks if an attendance record already exists for a specific member, class, and date.
        /// This is crucial for preventing duplicate attendance entries for the same class on the same day.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <param name="attendanceDate">The specific date to check for attendance (time part will be ignored).
        /// This should typically be the start date of the class.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if an attendance record exists, otherwise <c>false</c>.</returns>
        Task<bool> DoesAttendanceExistForMemberClassAndDateAsync(int memberId, int classId, DateTime attendanceDate); // ESTE ES EL MÉTODO NUEVO
    }
}