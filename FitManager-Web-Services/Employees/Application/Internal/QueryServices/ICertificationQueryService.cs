// Employees/Application/Internal/QueryServices/ICertificationQueryService.cs


using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Queries;
using System.Collections.Generic;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    /// <summary>
    /// Defines the contract for the Certification Query Service.
    /// This service handles the execution of queries related to Certifications.
    /// </summary>
    public interface ICertificationQueryService
    {
        /// <summary>
        /// Handles the GetAllCertificationsQuery to retrieve all certifications asynchronously.
        /// </summary>
        /// <param name="query">The query object for retrieving all certifications.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains
        /// an enumerable collection of Certification entities.</returns>
        Task<IEnumerable<Certification>> Handle(GetAllCertificationsQuery query);

    
    }
}