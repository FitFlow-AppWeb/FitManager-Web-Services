using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands
{
    /// <summary>
    /// Represents a command to update an existing fitness class's information.
    /// </summary>
    /// <remarks>
    /// This immutable record type (DTO) carries the unique identifier of the class
    /// along with its updated details. It's used to send modification requests
    /// from the application's external boundary to the Domain/Application layer.
    /// It implements <see cref="IRequest"/>, signaling that it's a command that doesn't
    /// return a specific value, but rather triggers a state change, typically handled by
    /// a corresponding command handler. All provided parameters will overwrite the existing class properties.
    /// </remarks>
    /// <param name="Id">The unique identifier of the class to update.</param>
    /// <param name="Name">The updated name of the class.</param>
    /// <param name="Description">The updated description of the class.</param>
    /// <param name="Type">The updated type or category of the class.</param>
    /// <param name="Capacity">The updated maximum capacity of attendees for the class.</param>
    /// <param name="StartDate">The updated scheduled start date and time of the class.</param>
    /// <param name="Duration">The updated duration of the class in minutes.</param>
    /// <param name="Status">The updated status of the class.</param>
    /// <param name="EmployeeId">The updated unique identifier of the employee (trainer/instructor) assigned to this class.</param>
    public record UpdateClassCommand(
        int Id,
        string Name,
        string Description,
        string Type,
        int Capacity,
        DateTime StartDate,
        int Duration,
        string Status,
        int EmployeeId
    ) : IRequest;
}