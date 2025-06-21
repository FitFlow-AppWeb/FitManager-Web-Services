using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands
{
    /// <summary>
    /// Represents a command to create a new fitness class.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that carries all the necessary
    /// information to schedule and define a new class. It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a command that expects a <see cref="Class"/> object as a response,
    /// typically after being handled by a corresponding command handler (e.g., in the Application layer).
    /// </remarks>
    /// <param name="Name">The name of the class (e.g., "Morning Yoga", "HIIT").</param>
    /// <param name="Description">A detailed description of the class.</param>
    /// <param name="Type">The type or category of the class (e.g., "Yoga", "Cardio").</param>
    /// <param name="Capacity">The maximum capacity of attendees for the class.</param>
    /// <param name="StartDate">The scheduled start date and time of the class.</param>
    /// <param name="Duration">The duration of the class in minutes.</param>
    /// <param name="Status">The initial status of the class (e.g., "Scheduled", "Active").</param>
    /// <param name="EmployeeId">The unique identifier of the employee (trainer/instructor) assigned to this class.</param>
    public record CreateClassCommand(
        string Name,
        string Description,
        string Type,
        int Capacity,
        DateTime StartDate,
        int Duration,
        string Status,
        int EmployeeId
    ) : IRequest<Class>;
}