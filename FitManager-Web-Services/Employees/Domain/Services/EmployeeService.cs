using System.Collections.Generic;
using System.Threading.Tasks;
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
            _unitOfWork = unitOfWork;
        }

        // Obtener todos los empleados
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return _employeeRepository.GetAllAsync();
        }

        // Obtener un empleado por ID
        public Task<Employee> GetByIdAsync(int id)
        {
            return _employeeRepository.GetByIdAsync(id);
        }

        // Obtener un empleado por DNI
        public Task<Employee> GetByDniAsync(int dni)
        {
            return _employeeRepository.GetByDniAsync(dni);
        }

        // Agregar un nuevo empleado
        public async Task AddAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync(); // Confirmar cambios
        }

        // Actualizar un empleado existente
        public async Task UpdateAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
            await _unitOfWork.CompleteAsync(); // Confirmar cambios
        }

        // Eliminar un empleado
        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync(); // Confirmar cambios
        }
    }
}