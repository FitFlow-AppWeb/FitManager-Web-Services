using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Employees.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for employee-related operations.
    /// </summary>
    /// <remarks>
    /// In Domain-Driven Design (DDD), domain services encapsulate business logic that
    /// doesn't naturally fit within a single aggregate, or orchestrates operations across multiple aggregates.
    /// This interface provides a facade for managing employee data, including CRUD operations
    /// and specific retrieval methods.
    /// </remarks>
    public interface IEmployeeService
    {
        /// <summary>
        /// Asynchronously retrieves all employees.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Employee"/> objects.
        /// </returns>
        Task<IEnumerable<Employee>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Employee"/> if found (or a default value if not, depending on implementation).
        /// </returns>
        Task<Employee> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves an employee by their DNI (National Identity Document).
        /// </summary>
        /// <param name="dni">The DNI of the employee.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Employee"/> if found (or a default value if not, depending on implementation).
        /// </returns>
        Task<Employee> GetByDniAsync(int dni);

        /// <summary>
        /// Asynchronously adds a new employee.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task AddAsync(Employee employee);

        /// <summary>
        /// Asynchronously updates an existing employee.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object with updated information.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAsync(Employee employee);

        /// <summary>
        /// Asynchronously deletes an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteAsync(int id);
    }
}