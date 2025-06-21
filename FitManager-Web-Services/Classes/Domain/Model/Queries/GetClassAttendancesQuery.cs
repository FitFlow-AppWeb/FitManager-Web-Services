using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic; 

namespace FitManager_Web_Services.Classes.Domain.Queries
{
    /// <summary>
    /// Represents a query to retrieve all attendance records for a specific class.
    /// </summary>
    /// <remarks>
    /// This immutable record type (DTO) carries the unique identifier of the class
    /// for which attendance records are requested. It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a query that expects an enumerable collection of <see cref="Attendance"/> objects
    /// as a response, typically after being handled by a corresponding query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.AttendanceQueryService"/> or <see cref="Application.Internal.QueryServices.ClassQueryService"/>).
    /// </remarks>
    /// <param name="ClassId">The unique identifier of the class whose attendance records are to be retrieved.</param>
    public record GetClassAttendancesQuery(int ClassId) : IRequest<IEnumerable<Attendance>>;
}