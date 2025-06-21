namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for creating a new attendance record, used for data transfer objects (DTOs) in RESTful API requests.
/// </summary>
/// <param name="EntryTime">The time when the member entered the class.</param>
/// <param name="ExitTime">The time when the member exited the class.</param>
/// <param name="MemberId">The unique identifier of the member associated with this attendance.</param>
/// <param name="ClassId">The unique identifier of the class associated with this attendance.</param>
public record CreateAttendanceResource(
    DateTime EntryTime,
    DateTime ExitTime,
    int MemberId,
    int ClassId);