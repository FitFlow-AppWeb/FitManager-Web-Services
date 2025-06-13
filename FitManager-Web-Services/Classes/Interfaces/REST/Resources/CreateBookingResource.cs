namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record CreateBookingResource(
    int MemberId,
    int ClassId,
    DateTime Date);