using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class CreateBookingCommandFromResourceAssembler
{
    public static CreateBookingCommand ToCommand(CreateBookingResource resource)
    {
        return new CreateBookingCommand(
            resource.MemberId,
            resource.ClassId,
            resource.Date);
    }
}