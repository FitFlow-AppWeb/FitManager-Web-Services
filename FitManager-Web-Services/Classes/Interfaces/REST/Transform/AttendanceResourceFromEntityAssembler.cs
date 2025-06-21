using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="AttendanceResource"/> from <see cref="Attendance"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// </remarks>
public static class AttendanceResourceFromEntityAssembler
{
    /// <summary>
    /// Converts an <see cref="Attendance"/> entity to an <see cref="AttendanceResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="Attendance"/> entity to convert.</param>
    /// <returns>A new <see cref="AttendanceResource"/> representing the converted entity.</returns>
    public static AttendanceResource ToResource(Attendance entity)
    {
        return new AttendanceResource(
            entity.Id,
            entity.EntryTime,
            entity.ExitTime,
            entity.MemberId,
            entity.ClassId);
    }
}