// Employees/Application/Internal/QueryServices/CertificationQueryService.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Queries;
using FitManager_Web_Services.Employees.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    /// <summary>
    /// Implements the Certification Query Service contract.
    /// Provides the concrete logic for handling Certification-related queries.
    /// </summary>
    public class CertificationQueryService : ICertificationQueryService
    {
        private readonly ICertificationRepository _certificationRepository;

        public CertificationQueryService(ICertificationRepository certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }

        public async Task<IEnumerable<Certification>> Handle(GetAllCertificationsQuery query)
        {
            return await _certificationRepository.GetAllAsync();
        }

   
    }
}