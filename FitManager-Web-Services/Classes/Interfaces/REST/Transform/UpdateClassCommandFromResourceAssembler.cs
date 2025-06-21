using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="UpdateClassCommand"/> from <see cref="UpdateClassResource"/>.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's command objects.
/// It translates the incoming <see cref="UpdateClassResource"/> (from an HTTP request) along with the ID
/// into an <see cref="UpdateClassCommand"/> that can be processed by the domain layer to update a class record.
/// </remarks>
public static class UpdateClassCommandFromResourceAssembler
{
    /// <summary>
    /// Converts an <see cref="UpdateClassResource"/> DTO and an ID to an <see cref="UpdateClassCommand"/> command.
    /// </summary>
    /// <param name="id">The unique identifier of the class to be updated.</param>
    /// <param name="resource">The <see cref="UpdateClassResource"/> containing the updated class details.</param>
    /// <returns>A new <see cref="UpdateClassCommand"/> representing the command to update a class.</returns>
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