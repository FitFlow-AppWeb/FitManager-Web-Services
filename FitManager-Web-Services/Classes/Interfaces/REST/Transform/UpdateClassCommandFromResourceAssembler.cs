using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class UpdateClassCommandFromResourceAssembler
{
    public static UpdateClassCommand ToCommand(int id, UpdateClassResource resource)
    {
        return new UpdateClassCommand(
            id,
            resource.Name,
            resource.Description,
            resource.Type,
            resource.Capacity,
            resource.StartDate,
            resource.Duration,
            resource.Status,
            resource.EmployeeId);
    }
}