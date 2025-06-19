using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Application.Internal.QueryServices;

public class AttendanceQueryService
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceQueryService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId)
    {
        return await _attendanceRepository.FindByClassAsync(classId);
    }

    public async Task<IEnumerable<Attendance>> GetAttendancesByMemberAsync(int memberId)
    {
        return await _attendanceRepository.FindByMemberAsync(memberId);
    }
}