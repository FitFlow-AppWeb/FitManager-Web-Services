using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;

namespace FitManager_Web_Services.Classes.Domain.Queries
{
    /// <summary>
    /// Represents a query to retrieve all fitness classes.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all class records from the system. It implements <see cref="IRequest{TResponse}"/>,
    /// indicating that it is a query that expects an enumerable collection of <see cref="Class"/> objects
    /// as a response, typically after being handled by a corresponding query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.ClassQueryService"/>).
    /// </remarks>
    public record GetAllClassesQuery : IRequest<IEnumerable<Class>>;
}