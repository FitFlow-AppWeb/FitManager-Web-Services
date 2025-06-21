using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;

namespace FitManager_Web_Services.Classes.Domain.Queries
{
    /// <summary>
    /// Represents a query to retrieve all booking records for a specific member.
    /// </summary>
    /// <remarks>
    /// This immutable record type (DTO) carries the unique identifier of the member
    /// whose class bookings are requested. It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a query that expects an enumerable collection of <see cref="Booking"/> objects
    /// as a response, typically after being handled by a corresponding query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.BookingQueryService"/> or
    /// <see cref="FitManager_Web_Services.Members.Application.Internal.QueryServices.MemberQueryService"/>).
    /// </remarks>
    /// <param name="MemberId">The unique identifier of the member whose bookings are to be retrieved.</param>
    public record GetMemberBookingsQuery(int MemberId) : IRequest<IEnumerable<Booking>>;
}