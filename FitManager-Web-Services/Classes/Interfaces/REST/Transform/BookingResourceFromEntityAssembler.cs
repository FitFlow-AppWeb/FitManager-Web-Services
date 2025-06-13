using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class BookingResourceFromEntityAssembler
{
    public static BookingResource ToResource(Booking entity)
    {
        return new BookingResource(
            entity.Id,
            entity.MemberId,
            entity.ClassId,
            entity.Date);
    }
}