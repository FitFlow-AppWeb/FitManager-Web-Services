using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Employees.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork; // Asignar el unitOfWork
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return _employeeRepository.GetAllAsync();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            return _employeeRepository.GetByIdAsync(id);
        }

        public Task<Employee> GetByDniAsync(int dni)
        {
            return _employeeRepository.GetByDniAsync(dni);
        }

        public async Task AddAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}