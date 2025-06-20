using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

using System;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing salary payments.
    /// This service orchestrates business logic related to creating salary payment records.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling finance-related use cases
    /// concerning employee compensation.
    /// </summary>
    public class SalaryPaymentCommandService
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalaryPaymentCommandService"/> class.
        /// </summary>
        /// <param name="salaryPaymentRepository">The salary payment repository for managing payment data.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
        public SalaryPaymentCommandService(
            ISalaryPaymentRepository salaryPaymentRepository,
            IUnitOfWork unitOfWork)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new salary payment record for an employee.
        /// </summary>
        /// <remarks>
        /// This method creates a new <see cref="SalaryPayment"/> aggregate with the provided details,
        /// persists it to the database, and completes the unit of work.
        /// <para>
        /// **TODO:** A validation for the existence of the employee (via an <c>IEmployeeRepository</c>)
        /// should be added once the Employees Bounded Context is fully integrated to ensure
        /// that the <paramref name="employeeId"/> refers to a valid employee.
        /// </para>
        /// </remarks>
        /// <param name="date">The date of the salary payment.</param>
        /// <param name="amount">The amount of the salary payment.</param>
        /// <param name="method">The method used for the payment (e.g., "Bank Transfer", "Cash").</param>
        /// <param name="currency">The currency of the payment (e.g., "USD", "PEN").</param>
        /// <param name="employeeId">The unique identifier of the employee receiving the payment.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="SalaryPayment"/> aggregate.</returns>
        public async Task<SalaryPayment?> CreateAsync(
            DateTime date,
            float amount,
            string method,
            string currency,
            int employeeId)
        {
            // TODO: Validar existencia de Employee cuando BC Employees esté listo
            // var employee = await _employeeRepository.GetByIdAsync(employeeId);
            // if (employee == null) return null;

            var payment = new SalaryPayment(date, amount, method, currency, employeeId);
            await _salaryPaymentRepository.AddAsync(payment);
            await _unitOfWork.CompleteAsync();

            return payment;
        }
    }
}