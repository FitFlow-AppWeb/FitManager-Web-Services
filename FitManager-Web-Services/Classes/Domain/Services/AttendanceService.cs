using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using System; // Required for DateTime
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IAttendanceService"/> contract, providing concrete business logic
    /// for querying <see cref="Attendance"/> records.
    /// </summary>
    /// <remarks>
    /// This service acts as a mediator for attendance-related query operations.
    /// It interacts with <see cref="IAttendanceRepository"/> for data persistence.
    /// Operations that modify state (e.g., Register, Update) are intentionally
    /// excluded from this service, as they should be handled by Command Services/Handlers.
    /// </remarks>
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceService"/> class.
        /// </summary>
        /// <param name="attendanceRepository">The repository for <see cref="Attendance"/> entities.</param>
        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // --- QUERY METHODS (Keeping these as is) ---

        /// <summary>
        /// Asynchronously retrieves all attendance records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Attendance"/> objects
        /// associated with the specified class.
        /// </returns>
        public async Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId) =>
            await _attendanceRepository.FindByClassAsync(classId);

        /// <summary>
        /// Asynchronously retrieves all attendance records for a specific member.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Attendance"/> objects
        /// associated with the specified member.
        /// </returns>
        public async Task<IEnumerable<Attendance>> GetAttendancesByMemberAsync(int memberId) =>
            await _attendanceRepository.FindByMemberAsync(memberId);

        /// <summary>
        /// Asynchronously retrieves a single attendance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the attendance record.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Attendance"/> if found, otherwise <c>null</c>.
        /// </returns>
        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _attendanceRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all attendance records from the repository.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of all <see cref="Attendance"/> objects.
        /// </returns>
        public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync()
        {
            return await _attendanceRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously checks if an attendance record already exists for a specific member, class, and date.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <param name="attendanceDate">The specific date to check for attendance.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if an attendance record exists, otherwise <c>false</c>.</returns>
        public async Task<bool> DoesAttendanceExistForMemberClassAndDateAsync(int memberId, int classId, DateTime attendanceDate)
        {
            // We use .Date to compare only the date part, ignoring the time
            var existingAttendance = await _attendanceRepository.GetByMemberClassAndDateAsync(memberId, classId, attendanceDate.Date);
            return existingAttendance != null;
        }

        // --- REMOVED METHODS (These methods should be handled by AttendanceCommandService) ---
        // public async Task<Attendance> RegisterAttendanceAsync(DateTime entryTime, DateTime exitTime, int memberId, int classId) { ... }
        // public async Task UpdateAttendanceAsync(int id, DateTime entryTime, DateTime exitTime) { ... }
    }
}