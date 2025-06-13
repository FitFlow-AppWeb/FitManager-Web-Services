using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public static class AttendanceWithMemberResourceFromEntityAssembler
{
    public static AttendanceWithMemberResource ToResource(Attendance entity)
    {
        return new AttendanceWithMemberResource(
            entity.Id,
            entity.EntryTime,
            entity.ExitTime,
            entity.MemberId,
            $"{entity.Member?.FirstName} {entity.Member?.LastName}".Trim(),
            entity.ClassId,
            entity.Class?.Name ?? "N/A");
    }
}