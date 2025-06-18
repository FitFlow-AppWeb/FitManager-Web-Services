namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record AttendanceWithMemberResource(
    int Id,
    DateTime EntryTime,
    DateTime ExitTime,
    int MemberId,
    string MemberName,
    int ClassId,
    string ClassName);