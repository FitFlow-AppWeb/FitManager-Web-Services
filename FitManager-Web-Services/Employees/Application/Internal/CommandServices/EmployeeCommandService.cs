using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    public class EmployeeCommandService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICertificationRepository _certificationRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeCommandService(
            IEmployeeRepository employeeRepository,
            ICertificationRepository certificationRepository,
            ISpecialtyRepository specialtyRepository,
            IUnitOfWork unitOfWork
        )
        {
            _employeeRepository = employeeRepository;
            _certificationRepository = certificationRepository;
            _specialtyRepository = specialtyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee?> Handle(CreateEmployeeCommand command)
        {
            

            
            var employee = new Employee(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email,
                command.Password,  
                command.Wage,      
                command.Role,      
                command.WorkHours  
            );

            // Guardar el Employee en el repositorio
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return employee;
        }


        public async Task<Employee?> Handle(UpdateEmployeeCommand command)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(command.Id);
            if (existingEmployee == null) return null;

            // Actualizar los datos básicos del Employee
            existingEmployee.Update(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email,
                command.Password,  // Incluir la contraseña
                command.Wage,      // Incluir el salario
                command.Role,      // Incluir el rol
                command.WorkHours  // Incluir las horas de trabajo
            );

            
            // Guardar los cambios
            await _employeeRepository.UpdateAsync(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return existingEmployee;
        }


        public async Task<bool> Handle(DeleteEmployeeCommand command)
        {
            var employee = await _employeeRepository.GetByIdAsync(command.Id);
            if (employee == null) return false;

            await _employeeRepository.DeleteAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
