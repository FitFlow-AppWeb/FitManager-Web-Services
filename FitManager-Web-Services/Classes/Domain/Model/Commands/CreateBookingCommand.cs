using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using MediatR; 

namespace FitManager_Web_Services.Classes.Domain.Commands
{
    /// <summary>
    /// Represents a command to create a new class booking.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that encapsulates the necessary
    /// information for a member to book a class. It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a command that expects a <see cref="Booking"/> object as a response,
    /// typically after being handled by a corresponding command handler (e.g., in the Application layer).
    /// </remarks>
    /// <param name="MemberId">The unique identifier of the member making the booking.</param>
    /// <param name="ClassId">The unique identifier of the class being booked.</param>
    /// <param name="Date">The specific date and time for which the class is being booked.</param>
    public record CreateBookingCommand(
        int MemberId,
        int ClassId,
        DateTime Date
    ) : IRequest<Booking>;
}