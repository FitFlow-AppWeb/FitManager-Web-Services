using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class CreateAttendanceCommandFromResourceAssembler
{
    public static RegisterAttendanceCommand ToCommand(CreateAttendanceResource resource)
    {
        return new RegisterAttendanceCommand(
            resource.EntryTime,
            resource.ExitTime,
            resource.MemberId,
            resource.ClassId);
    }
}