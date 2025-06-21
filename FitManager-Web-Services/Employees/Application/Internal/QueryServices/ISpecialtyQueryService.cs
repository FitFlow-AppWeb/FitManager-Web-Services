// Employees/Application/Internal/QueryServices/ISpecialtyQueryService.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Queries;
using System.Collections.Generic;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    /// <summary>
    /// Defines the contract for the Specialty Query Service.
    /// This service handles the execution of queries related to Specialties.
    /// </summary>
    public interface ISpecialtyQueryService
    {
        /// <summary>
        /// Handles the GetAllSpecialtiesQuery to retrieve all specialties asynchronously.
        /// </summary>
        /// <param name="query">The query object for retrieving all specialties.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains
        /// an enumerable collection of Specialty entities.</returns>
        Task<IEnumerable<Specialty>> Handle(GetAllSpecialtiesQuery query);

      
    }
}