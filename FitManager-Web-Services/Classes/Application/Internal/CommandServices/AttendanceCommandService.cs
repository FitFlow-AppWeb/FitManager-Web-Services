using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices;

public class AttendanceCommandService
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceCommandService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Attendance> RegisterAttendanceAsync(DateTime entryTime, DateTime exitTime, int memberId, int classId)
    {
        var attendance = new Attendance(entryTime, exitTime, memberId, classId);
        await _attendanceRepository.AddAsync(attendance);
        return attendance;
    }

    public async Task UpdateAttendanceAsync(int id, DateTime entryTime, DateTime exitTime)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);
        if (attendance == null) throw new Exception("Attendance not found");
        
        attendance.EntryTime = entryTime;
        attendance.ExitTime = exitTime;
        
        await _attendanceRepository.UpdateAsync(attendance);
    }
}