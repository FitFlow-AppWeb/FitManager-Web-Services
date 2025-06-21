using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Queries
{
    /// <summary>
    /// Represents a query to retrieve a single fitness class by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This immutable record type (DTO) carries the unique identifier of the specific class
    /// to be retrieved from the system. It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a query that expects a single <see cref="Class"/> object (which might be null if not found)
    /// as a response. This query is typically handled by a corresponding query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.ClassQueryService"/>).
    /// </remarks>
    /// <param name="Id">The unique identifier of the class to retrieve.</param>
    public record GetClassByIdQuery(int Id) : IRequest<Class?>;
}