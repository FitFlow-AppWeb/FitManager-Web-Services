namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for a class booking, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the booking record.</param>
/// <param name="MemberId">The unique identifier of the member who made the booking.</param>
/// <param name="ClassId">The unique identifier of the class that was booked.</param>
/// <param name="Date">The date and time when the class is booked for.</param>
public record BookingResource(
    int Id,
    int MemberId,
    int ClassId,
    DateTime Date);