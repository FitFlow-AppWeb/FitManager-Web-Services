namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for updating an existing attendance record, used for data transfer objects (DTOs) in RESTful API requests.
/// </summary>
/// <param name="EntryTime">The updated time when the member entered the class.</param>
/// <param name="ExitTime">The updated time when the member exited the class.</param>
public record UpdateAttendanceResource(
    DateTime EntryTime,
    DateTime ExitTime);