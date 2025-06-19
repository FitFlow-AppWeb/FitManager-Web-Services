namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record UpdateAttendanceResource(
    DateTime EntryTime,
    DateTime ExitTime);