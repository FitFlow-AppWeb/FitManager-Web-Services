using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;
using FitManager_Web_Services.Employees.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices;

/// <summary>
/// Represents the command service for managing salary payment operations.
/// </summary>
/// <remarks>
/// This service handles commands that result in changes to the state of salary payments.
/// It orchestrates interactions between the <see cref="ISalaryPaymentRepository"/> for persistence,
/// <see cref="IUnitOfWork"/> for transactional consistency, and <see cref="IEmployeeRepository"/>
/// to validate employee existence.
/// It belongs to the Application layer in Clean Architecture, responsible for handling write use cases.
/// </remarks>
public class SalaryPaymentCommandService
{
    private readonly ISalaryPaymentRepository _salaryPaymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalaryPaymentCommandService"/> class.
    /// </summary>
    /// <param name="salaryPaymentRepository">The repository for salary payments.</param>
    /// <param name="unitOfWork">The unit of work to manage atomic operations.</param>
    /// <param name="employeeRepository">The repository for employee data, used for validation.</param>
    public SalaryPaymentCommandService(
        ISalaryPaymentRepository salaryPaymentRepository,
        IUnitOfWork unitOfWork,
        IEmployeeRepository employeeRepository)
    {
        _salaryPaymentRepository = salaryPaymentRepository;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Asynchronously creates and registers a new salary payment.
    /// </summary>
    /// <param name="date">The date of the salary payment.</param>
    /// <param name="amount">The amount of the salary payment.</param>
    /// <param name="method">The payment method (e.g., "Bank Transfer", "Cash").</param>
    /// <param name="currency">The currency of the payment (e.g., "USD", "PEN").</param>
    /// <param name="employeeId">The unique identifier of the employee to whom the salary is paid.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the newly created <see cref="SalaryPayment"/> object if successful,
    /// otherwise <c>null</c> if the specified employee does not exist.
    /// </returns>
    public async Task<SalaryPayment?> CreateAsync(
        DateTime date,
        float amount,
        string method,
        string currency,
        int employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null) return null;
        
        var payment = new SalaryPayment(date, amount, method, currency, employeeId);
        await _salaryPaymentRepository.AddAsync(payment);
        await _unitOfWork.CompleteAsync();

        return payment;
    }
}