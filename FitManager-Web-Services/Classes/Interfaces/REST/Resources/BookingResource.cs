namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record BookingResource(
    int Id,
    int MemberId,
    int ClassId,
    DateTime Date);