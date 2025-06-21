using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving salary payment information.
    /// </summary>
    /// <remarks>
    /// This service handles queries related to employee salary payments and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// It interacts with <see cref="ISalaryPaymentRepository"/> to fetch data without modifying the system state.
    /// </remarks>
    public class SalaryPaymentQueryService
    {
        private readonly ISalaryPaymentRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalaryPaymentQueryService"/> class.
        /// </summary>
        /// <param name="repository">The salary payment repository for data access.</param>
        public SalaryPaymentQueryService(ISalaryPaymentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Asynchronously retrieves all salary payment records.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all <see cref="SalaryPayment"/> aggregates to the underlying <see cref="ISalaryPaymentRepository"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of all <see cref="SalaryPayment"/> objects found in the repository.
        /// </returns>
        public async Task<IEnumerable<SalaryPayment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        
        /// <summary>
        /// Asynchronously retrieves a collection of salary payments made to a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee whose salary payments are to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="SalaryPayment"/> objects
        /// associated with the specified <paramref name="employeeId"/>.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Thrown if the underlying repository does not implement the necessary method.</exception>
        public async Task<IEnumerable<SalaryPayment>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _repository.GetByEmployeeIdAsync(employeeId);
        }
    }
}