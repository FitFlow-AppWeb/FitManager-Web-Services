using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class AttendanceResourceFromEntityAssembler
{
    public static AttendanceResource ToResource(Attendance entity)
    {
        return new AttendanceResource(
            entity.Id,
            entity.EntryTime,
            entity.ExitTime,
            entity.MemberId,
            entity.ClassId);
    }
}