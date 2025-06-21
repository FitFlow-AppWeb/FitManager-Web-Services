using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using MediatR; 

namespace FitManager_Web_Services.Classes.Domain.Commands
{
    /// <summary>
    /// Represents a command to register a new attendance record for a member in a class.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that encapsulates the necessary
    /// information to record a member's presence in a specific class session, including their entry and exit times.
    /// It implements <see cref="IRequest{TResponse}"/>, indicating that it is a command that expects an
    /// <see cref="Attendance"/> object as a response, typically after being handled by a corresponding command handler
    /// (e.g., in the Application layer).
    /// </remarks>
    /// <param name="EntryTime">The precise date and time when the member entered the class.</param>
    /// <param name="ExitTime">The precise date and time when the member exited the class.</param>
    /// <param name="MemberId">The unique identifier of the member whose attendance is being registered.</param>
    /// <param name="ClassId">The unique identifier of the class for which the attendance is being recorded.</param>
    public record RegisterAttendanceCommand(
        DateTime EntryTime,
        DateTime ExitTime,
        int MemberId,
        int ClassId
    ) : IRequest<Attendance>;
}