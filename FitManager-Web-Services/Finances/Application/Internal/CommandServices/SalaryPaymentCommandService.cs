using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;
using FitManager_Web_Services.Employees.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.CommandServices;

public class SalaryPaymentCommandService
{
    private readonly ISalaryPaymentRepository _salaryPaymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;

    public SalaryPaymentCommandService(
        ISalaryPaymentRepository salaryPaymentRepository,
        IUnitOfWork unitOfWork,
        IEmployeeRepository employeeRepository)
    {
        _salaryPaymentRepository = salaryPaymentRepository;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }

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