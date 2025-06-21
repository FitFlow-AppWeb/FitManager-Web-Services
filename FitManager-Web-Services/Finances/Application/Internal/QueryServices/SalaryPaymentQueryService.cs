using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving salary payment information.
    /// This service handles queries related to employee salary payments and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class SalaryPaymentQueryService
    {
        private readonly ISalaryPaymentRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalaryPaymentQueryService"/> class.
        /// </summary>
        /// <param name="repository">The salary payment repository.</param>
        public SalaryPaymentQueryService(ISalaryPaymentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all salary payment records.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all <see cref="SalaryPayment"/> aggregates to the <see cref="ISalaryPaymentRepository"/>.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of all <see cref="SalaryPayment"/> objects.</returns>
        public async Task<IEnumerable<SalaryPayment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
    
    public async Task<IEnumerable<SalaryPayment>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _repository.GetByEmployeeIdAsync(employeeId);
    }
}