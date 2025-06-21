namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for creating a new class booking, used for data transfer objects (DTOs) in RESTful API requests.
/// </summary>
/// <param name="MemberId">The unique identifier of the member making the booking.</param>
/// <param name="ClassId">The unique identifier of the class being booked.</param>
/// <param name="Date">The date and time for which the class is being booked.</param>
public record CreateBookingResource(
    int MemberId,
    int ClassId,
    DateTime Date);