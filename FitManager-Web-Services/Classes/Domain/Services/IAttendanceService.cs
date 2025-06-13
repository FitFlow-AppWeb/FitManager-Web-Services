using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Services;

public interface IAttendanceService
{
    Task<Attendance> RegisterAttendanceAsync(DateTime entryTime, DateTime exitTime, int memberId, int classId);
    Task UpdateAttendanceAsync(int id, DateTime entryTime, DateTime exitTime);
    Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId);
    Task<IEnumerable<Attendance>> GetAttendancesByMemberAsync(int memberId);
    Task<Attendance?> GetAttendanceByIdAsync(int id);
}