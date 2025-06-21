using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands
{
    /// <summary>
    /// Represents a command to delete an existing class.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that carries the unique identifier
    /// of the class to be removed from the system. It implements <see cref="IRequest"/>,
    /// indicating that it is a command that does not return a specific result, typically handled by a corresponding
    /// command handler (e.g., in the Application layer).
    /// </remarks>
    /// <param name="Id">The unique identifier of the class to be deleted.</param>
    public record DeleteClassCommand(int Id) : IRequest;
}