using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Employees.Domain.Model.Queries;

namespace FitManager_Web_Services.Employees.Application.Internal.QueryServices
{
    public class EmployeeQueryService
    {
        private readonly IEmployeeRepository _employeeRepository;

        // Constructor que inyecta el repositorio de empleados
        public EmployeeQueryService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Obtener todos los empleados
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery query)
        {
            return await _employeeRepository.GetAllAsync();
        }

        // Obtener un empleado por ID
        public async Task<Employee?> Handle(GetEmployeeByIdQuery query)
        {
            return await _employeeRepository.GetByIdAsync(query.Id);
        }

        // Obtener un empleado por DNI
        public async Task<Employee?> Handle(GetEmployeeByDniQuery query)
        {
            return await _employeeRepository.GetByDniAsync(query.Dni);
        }
    }
}