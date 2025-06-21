namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents a resource for an attendance record that includes details of the associated member and class.
/// This DTO is used to provide richer information in RESTful API responses.
/// </summary>
/// <param name="Id">The unique identifier of the attendance record.</param>
/// <param name="EntryTime">The time when the member entered the class.</param>
/// <param name="ExitTime">The time when the member exited the class.</param>
/// <param name="MemberId">The unique identifier of the member associated with this attendance.</param>
/// <param name="MemberName">The name of the member associated with this attendance.</param>
/// <param name="ClassId">The unique identifier of the class associated with this attendance.</param>
/// <param name="ClassName">The name of the class associated with this attendance.</param>
public record AttendanceWithMemberResource(
    int Id,
    DateTime EntryTime,
    DateTime ExitTime,
    int MemberId,
    string MemberName,
    int ClassId,
    string ClassName);