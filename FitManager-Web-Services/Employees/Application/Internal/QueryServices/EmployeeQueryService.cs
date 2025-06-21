using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Employees.Domain.Model.Queries;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving employee information.
    /// </summary>
    /// <remarks>
    /// This service handles read-only operations related to employee data,
    /// fetching information from the <see cref="IEmployeeRepository"/>.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling query (read) use cases.
    /// </remarks>
    public class EmployeeQueryService
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeQueryService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository for <see cref="Employee"/> entities.</param>
        public EmployeeQueryService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Asynchronously handles the <see cref="GetAllEmployeesQuery"/> to retrieve all employees
        /// along with their certifications and specialties.
        /// </summary>
        /// <param name="query">The <see cref="GetAllEmployeesQuery"/> instance.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Employee"/> objects,
        /// including their associated certifications and specialties.
        /// </returns>
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery query)
        {
            return await _employeeRepository.GetAllWithCertificationsAndSpecialtiesAsync();
        }
    }
}