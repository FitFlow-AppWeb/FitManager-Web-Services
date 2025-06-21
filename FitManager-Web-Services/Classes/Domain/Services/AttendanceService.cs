using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IAttendanceService"/> contract, providing concrete business logic
    /// for managing <see cref="Attendance"/> records.
    /// </summary>
    /// <remarks>
    /// This service acts as a mediator for attendance-related operations, handling the creation,
    /// updating, and retrieval of attendance records. It interacts with <see cref="IAttendanceRepository"/>
    /// for data persistence.
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
        public async Task<Attendance> RegisterAttendanceAsync(DateTime entryTime, DateTime exitTime, int memberId, int classId)
        {
            var attendance = new Attendance(entryTime, exitTime, memberId, classId);
            await _attendanceRepository.AddAsync(attendance);
            return attendance;
        }

        /// <summary>
        /// Asynchronously updates the entry and exit times of an existing attendance record.
        /// </summary>
        /// <param name="id">The unique identifier of the attendance record to update.</param>
        /// <param name="entryTime">The updated entry time for the attendance record.</param>
        /// <param name="exitTime">The updated exit time for the attendance record.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the attendance record with the specified ID is not found.</exception>
        public async Task UpdateAttendanceAsync(int id, DateTime entryTime, DateTime exitTime)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                throw new Exception("Attendance not found");
            }
            
            attendance.EntryTime = entryTime;
            attendance.ExitTime = exitTime;
            
            await _attendanceRepository.UpdateAsync(attendance);
        }

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
    }
}