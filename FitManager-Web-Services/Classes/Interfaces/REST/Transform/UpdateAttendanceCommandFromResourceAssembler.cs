using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="UpdateAttendanceCommand"/> from <see cref="UpdateAttendanceResource"/>.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's command objects.
/// It translates the incoming <see cref="UpdateAttendanceResource"/> (from an HTTP request) along with the ID
/// into an <see cref="UpdateAttendanceCommand"/> that can be processed by the domain layer to update an attendance record.
/// </remarks>
public static class UpdateAttendanceCommandFromResourceAssembler
{
    /// <summary>
    /// Converts an <see cref="UpdateAttendanceResource"/> DTO and an ID to an <see cref="UpdateAttendanceCommand"/> command.
    /// </summary>
    /// <param name="id">The unique identifier of the attendance record to be updated.</param>
    /// <param name="resource">The <see cref="UpdateAttendanceResource"/> containing the updated attendance details.</param>
    /// <returns>A new <see cref="UpdateAttendanceCommand"/> representing the command to update an attendance record.</returns>
    public static UpdateAttendanceCommand ToCommand(int id, UpdateAttendanceResource resource)
    {
        return new UpdateAttendanceCommand(
            id,
            resource.EntryTime,
            resource.ExitTime);
    }
}