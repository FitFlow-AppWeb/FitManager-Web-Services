using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Application.Internal.QueryServices;

public class SalaryPaymentQueryService
{
    private readonly ISalaryPaymentRepository _repository;

    public SalaryPaymentQueryService(ISalaryPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SalaryPayment>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    
    public async Task<IEnumerable<SalaryPayment>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _repository.GetByEmployeeIdAsync(employeeId);
    }
}