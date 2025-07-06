// Classes/Interfaces/REST/Transform/RawAttendanceResourceFromEntityAssembler.cs

using FitManager_Web_Services.Classes.Domain.Model.Aggregates;  
using FitManager_Web_Services.Classes.Interfaces.REST.Resources; 

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public class RawAttendanceResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a single <see cref="Attendance"/> entity to a <see cref="RawAttendanceResource"/>.
    /// </summary>
    /// <param name="entity">The Attendance domain entity.</param>
    /// <returns>A RawAttendanceResource representing the entity.</returns>
    public RawAttendanceResource ToResourceFromEntity(Attendance entity)
    {
        return new RawAttendanceResource
        {
            Id = entity.Id,
            EntryTime = entity.EntryTime,
            ExitTime = entity.ExitTime, 
            MemberId = entity.MemberId,
            ClassId = entity.ClassId
        };
    }

    /// <summary>
    /// Converts a collection of <see cref="Attendance"/> entities to an enumerable collection of <see cref="RawAttendanceResource"/>.
    /// </summary>
    /// <param name="entities">The collection of Attendance domain entities.</param>
    /// <returns>An IEnumerable of RawAttendanceResource representing the entities.</returns>
    public IEnumerable<RawAttendanceResource> ToResourcesFromEntities(IEnumerable<Attendance> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}