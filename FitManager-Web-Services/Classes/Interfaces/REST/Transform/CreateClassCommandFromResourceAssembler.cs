using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="CreateClassCommand"/> from <see cref="CreateClassResource"/>.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's command objects.
/// It translates the incoming <see cref="CreateClassResource"/> (from an HTTP request) into a
/// <see cref="CreateClassCommand"/> that can be processed by the domain layer.
/// </remarks>
public static class CreateClassCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateClassResource"/> DTO to a <see cref="CreateClassCommand"/> command.
    /// </summary>
    /// <param name="resource">The <see cref="CreateClassResource"/> to convert.</param>
    /// <returns>A new <see cref="CreateClassCommand"/> representing the command to create a class.</returns>
    public static CreateClassCommand ToCommand(CreateClassResource resource)
    {
        return new CreateClassCommand(
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