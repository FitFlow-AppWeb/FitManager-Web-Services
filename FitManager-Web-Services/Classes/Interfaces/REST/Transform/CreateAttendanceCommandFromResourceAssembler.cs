using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="RegisterAttendanceCommand"/> from <see cref="CreateAttendanceResource"/>.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's command objects.
/// It translates the incoming <see cref="CreateAttendanceResource"/> (from an HTTP request) into a
/// <see cref="RegisterAttendanceCommand"/> that can be processed by the domain layer.
/// </remarks>
public static class CreateAttendanceCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateAttendanceResource"/> DTO to a <see cref="RegisterAttendanceCommand"/> command.
    /// </summary>
    /// <param name="resource">The <see cref="CreateAttendanceResource"/> to convert.</param>
    /// <returns>A new <see cref="RegisterAttendanceCommand"/> representing the command to register attendance.</returns>
    public static RegisterAttendanceCommand ToCommand(CreateAttendanceResource resource)
    {
        return new RegisterAttendanceCommand(
            resource.EntryTime,
            resource.ExitTime,
            resource.MemberId,
            resource.ClassId);
    }
}