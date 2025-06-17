using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Employees.Domain.Model.Queries;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    public class EmployeeQueryService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeQueryService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery query)
        {
            return await _employeeRepository.GetAllAsync();
        }

    }
}