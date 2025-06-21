using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="CreateBookingCommand"/> from <see cref="CreateBookingResource"/>.
/// </summary>
/// <remarks>
/// This assembler translates an incoming <see cref="CreateBookingResource"/> (from an HTTP request)
/// into a <see cref="CreateBookingCommand"/> that the domain layer can process. It acts as a bridge
/// between the API's data transfer objects (DTOs) and the domain's command objects.
/// </remarks>
public static class CreateBookingCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateBookingResource"/> DTO to a <see cref="CreateBookingCommand"/> command.
    /// </summary>
    /// <param name="resource">The <see cref="CreateBookingResource"/> to convert.</param>
    /// <returns>A new <see cref="CreateBookingCommand"/> representing the command to create a booking.</returns>
    public static CreateBookingCommand ToCommand(CreateBookingResource resource)
    {
        return new CreateBookingCommand(
            resource.MemberId,
            resource.ClassId,
            resource.Date);
    }
}