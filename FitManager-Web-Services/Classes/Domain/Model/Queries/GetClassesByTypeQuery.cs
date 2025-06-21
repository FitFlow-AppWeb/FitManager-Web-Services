using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic; 

namespace FitManager_Web_Services.Classes.Domain.Queries
{
    /// <summary>
    /// Represents a query to retrieve all fitness classes of a specific type.
    /// </summary>
    /// <remarks>
    /// This immutable record type (DTO) carries the type string, allowing for filtering
    /// classes based on their category (e.g., "Yoga", "Cardio"). It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a query that expects an enumerable collection of <see cref="Class"/> objects
    /// as a response. This query is typically handled by a corresponding query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.ClassQueryService"/>).
    /// </remarks>
    /// <param name="Type">The type or category of classes to retrieve (e.g., "Yoga", "Cardio").</param>
    public record GetClassesByTypeQuery(string Type) : IRequest<IEnumerable<Class>>;
}