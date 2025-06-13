namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record AttendanceResource(
    int Id,
    DateTime EntryTime,
    DateTime ExitTime,
    int MemberId,
    int ClassId);