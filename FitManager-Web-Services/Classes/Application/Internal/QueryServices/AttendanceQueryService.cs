using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories; 

using System.Collections.Generic;
using System.Threading.Tasks; 

namespace FitManager_Web_Services.Classes.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving attendance information.
    /// This service handles queries related to attendance records and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases
    /// specific to attendance linked to classes.
    /// </summary>
    public class AttendanceQueryService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceQueryService"/> class.
        /// </summary>
        /// <param name="attendanceRepository">The attendance repository for data access operations.</param>
        public AttendanceQueryService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        /// <summary>
        /// Retrieves all attendance records for a specific class.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of attendance records filtered by class ID to the <see cref="IAttendanceRepository"/>.
        /// </remarks>
        /// <param name="classId">The unique identifier of the class whose attendance records are to be retrieved.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Attendance"/> objects associated with the specified class.</returns>
        public async Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId)
        {
            return await _attendanceRepository.FindByClassAsync(classId);
        }

        /// <summary>
        /// Retrieves all attendance records for a specific member.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of attendance records filtered by member ID to the <see cref="IAttendanceRepository"/>.
        /// </remarks>
        /// <param name="memberId">The unique identifier of the member whose attendance records are to be retrieved.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Attendance"/> objects associated with the specified member.</returns>
        public async Task<IEnumerable<Attendance>> GetAttendancesByMemberAsync(int memberId)
        {
            return await _attendanceRepository.FindByMemberAsync(memberId);
        }
    }
}