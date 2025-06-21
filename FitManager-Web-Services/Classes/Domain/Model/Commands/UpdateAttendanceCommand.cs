using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands
{
    /// <summary>
    /// Represents a command to update an existing attendance record.
    /// </summary>
    /// <remarks>
    /// This immutable record type (DTO) carries the unique identifier of the attendance record
    /// along with the updated entry and exit times. It's used to send modification requests
    /// from the application's external boundary to the Domain/Application layer.
    /// It implements <see cref="IRequest"/>, signaling that it's a command that doesn't
    /// return a specific value, but rather triggers a state change, typically handled by
    /// a corresponding command handler.
    /// </remarks>
    /// <param name="Id">The unique identifier of the attendance record to update.</param>
    /// <param name="EntryTime">The updated entry time for the attendance record.</param>
    /// <param name="ExitTime">The updated exit time for the attendance record.</param>
    public record UpdateAttendanceCommand(
        int Id,
        DateTime EntryTime,
        DateTime ExitTime) : IRequest;
}