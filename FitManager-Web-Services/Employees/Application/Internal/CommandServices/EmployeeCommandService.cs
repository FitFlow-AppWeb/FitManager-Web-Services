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
            // Obtener certificaciones y especialidades por sus IDs
            var certifications = await _certificationRepository.GetByIdsAsync(command.CertificationIds);
            var specialties = await _specialtyRepository.GetByIdsAsync(command.SpecialtyIds);

            if (certifications == null || specialties == null)
            {
                return null; 
            }

            // Crear el nuevo Employee
            var employee = new Employee(
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

            // Asignar certificaciones y especialidades
            employee.AssignCertifications(certifications);
            employee.AssignSpecialties(specialties);

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

            // Si hay cambios en las certificaciones y especialidades, actualizar
            if (command.CertificationIds != null)
            {
                var certifications = await _certificationRepository.GetByIdsAsync(command.CertificationIds);
                existingEmployee.AssignCertifications(certifications);
            }

            if (command.SpecialtyIds != null)
            {
                var specialties = await _specialtyRepository.GetByIdsAsync(command.SpecialtyIds);
                existingEmployee.AssignSpecialties(specialties);
            }

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
