using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

using System;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing attendance records within the context of classes.
    /// This service orchestrates business logic related to registering and updating attendance.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling use cases
    /// specific to attendance linked to classes.
    /// </summary>
    public class AttendanceCommandService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceCommandService"/> class.
        /// </summary>
        /// <param name="attendanceRepository">The attendance repository for data access operations.</param>
        public AttendanceCommandService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        /// <summary>
        /// Registers a new attendance record for a member in a specific class.
        /// </summary>
        /// <remarks>
        /// This method creates a new <see cref="Attendance"/> aggregate with the provided details
        /// and persists it to the database using the attendance repository.
        /// </remarks>
        /// <param name="entryTime">The time when the member entered the class.</param>
        /// <param name="exitTime">The time when the member exited the class.</param>
        /// <param name="memberId">The unique identifier of the member attending the class.</param>
        /// <param name="classId">The unique identifier of the class attended.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="Attendance"/> aggregate.</returns>
        public async Task<Attendance> RegisterAttendanceAsync(DateTime entryTime, DateTime exitTime, int memberId, int classId)
        {
            var attendance = new Attendance(entryTime, exitTime, memberId, classId);
            await _attendanceRepository.AddAsync(attendance);
            return attendance;
        }

        /// <summary>
        /// Updates an existing attendance record.
        /// </summary>
        /// <remarks>
        /// This method retrieves an <see cref="Attendance"/> aggregate by its ID,
        /// updates its entry and exit times, and then persists these changes.
        /// Throws an <see cref="Exception"/> if the attendance record is not found.
        /// </remarks>
        /// <param name="id">The unique identifier of the attendance record to update.</param>
        /// <param name="entryTime">The new entry time for the attendance record.</param>
        /// <param name="exitTime">The new exit time for the attendance record.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the attendance record with the specified ID is not found.</exception>
        public async Task UpdateAttendanceAsync(int id, DateTime entryTime, DateTime exitTime)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null) throw new Exception("Attendance not found");
            
            attendance.EntryTime = entryTime;
            attendance.ExitTime = exitTime;
            
            await _attendanceRepository.UpdateAsync(attendance);
        }
    }
}