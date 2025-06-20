using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Employees.Domain.Model.Queries;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving employee information.
    /// This service handles queries related to employees and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class EmployeeQueryService
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeQueryService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        public EmployeeQueryService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all employees.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all employees to the <see cref="IEmployeeRepository"/>.
        /// </remarks>
        /// <param name="query">The query object for retrieving all employees.</param>
        /// <returns>An asynchronous operation that returns an enumerable collection of <see cref="Employee"/> objects.</returns>
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery query)
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}