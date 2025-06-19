namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record CreateAttendanceResource(
    DateTime EntryTime,
    DateTime ExitTime,
    int MemberId,
    int ClassId);