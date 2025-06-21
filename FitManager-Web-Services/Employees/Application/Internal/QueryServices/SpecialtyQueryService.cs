// Employees/Application/Internal/QueryServices/SpecialtyQueryService.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Queries;
using FitManager_Web_Services.Employees.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    /// <summary>
    /// Implements the Specialty Query Service contract.
    /// Provides the concrete logic for handling Specialty-related queries.
    /// </summary>
    public class SpecialtyQueryService : ISpecialtyQueryService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyQueryService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IEnumerable<Specialty>> Handle(GetAllSpecialtiesQuery query)
        {
            return await _specialtyRepository.GetAllAsync();
        }

     
    }
}