using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Employees.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IEmployeeService"/> contract, providing concrete business logic
    /// for managing <see cref="Employee"/> entities.
    /// </summary>
    /// <remarks>
    /// This service acts as a mediator between the application layer and the persistence layer,
    /// orchestrating operations on employee data. It uses an <see cref="IEmployeeRepository"/>
    /// for data access and an <see cref="IUnitOfWork"/> to ensure transactional consistency
    /// for write operations.
    /// </remarks>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository for <see cref="Employee"/> entities.</param>
        /// <param name="unitOfWork">The unit of work to manage transactional operations.</param>
        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Asynchronously retrieves all employees.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Employee"/> objects.</returns>
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return _employeeRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Employee"/> if found, otherwise <c>null</c>.</returns>
        public Task<Employee> GetByIdAsync(int id)
        {
            return _employeeRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves an employee by their DNI (National Identity Document).
        /// </summary>
        /// <param name="dni">The DNI of the employee.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Employee"/> if found, otherwise <c>null</c>.</returns>
        public Task<Employee> GetByDniAsync(int dni)
        {
            return _employeeRepository.GetByDniAsync(dni);
        }

        /// <summary>
        /// Asynchronously adds a new employee to the repository and completes the unit of work.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task AddAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing employee in the repository and completes the unit of work.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object with updated information.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task UpdateAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Asynchronously deletes an employee by their unique identifier from the repository and completes the unit of work.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}