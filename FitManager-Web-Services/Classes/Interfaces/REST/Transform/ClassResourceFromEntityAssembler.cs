using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class ClassResourceFromEntityAssembler
{
    public static ClassResource ToResource(Class entity)
    {
        return new ClassResource(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Type,
            entity.Capacity,
            entity.StartDate,
            entity.Duration,
            entity.Status,
            entity.EmployeeId);
    }
}
