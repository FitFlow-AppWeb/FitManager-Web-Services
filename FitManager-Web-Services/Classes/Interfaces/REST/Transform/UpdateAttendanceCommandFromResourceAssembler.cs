using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class UpdateAttendanceCommandFromResourceAssembler
{
    public static UpdateAttendanceCommand ToCommand(int id, UpdateAttendanceResource resource)
    {
        return new UpdateAttendanceCommand(
            id,
            resource.EntryTime,
            resource.ExitTime);
    }
}