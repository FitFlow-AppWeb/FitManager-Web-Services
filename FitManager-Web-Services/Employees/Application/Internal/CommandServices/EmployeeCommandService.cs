using System.Threading.Tasks;
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

        public EmployeeCommandService(IEmployeeRepository employeeRepository, 
                                      ICertificationRepository certificationRepository, 
                                      ISpecialtyRepository specialtyRepository, 
                                      IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _certificationRepository = certificationRepository;
            _specialtyRepository = specialtyRepository;
            _unitOfWork = unitOfWork;
        }

        // Manejo de la creación de un empleado
        public async Task<Employee?> Handle(CreateEmployeeCommand command)
        {
            var employee = new Employee(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email
            );

            // Asignar las certificaciones y especialidades si existen en el comando
            if (command.CertificationIds != null && command.CertificationIds.Count > 0)
            {
                foreach (var certId in command.CertificationIds)
                {
                    var certification = await _certificationRepository.GetByIdAsync(certId);
                    if (certification != null)
                    {
                        employee.Certifications.Add(certification);
                    }
                }
            }

            if (command.SpecialtyIds != null && command.SpecialtyIds.Count > 0)
            {
                foreach (var specId in command.SpecialtyIds)
                {
                    var specialty = await _specialtyRepository.GetByIdAsync(specId);
                    if (specialty != null)
                    {
                        employee.Specialties.Add(specialty);
                    }
                }
            }

            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return employee;
        }

        // Manejo de la actualización de un empleado
        public async Task<Employee?> Handle(UpdateEmployeeCommand command)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(command.Id);
            if (existingEmployee == null) return null;

            existingEmployee.Update(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email
            );

            // Si el comando incluye nuevas certificaciones o especialidades, se asignan
            if (command.CertificationIds != null && command.CertificationIds.Count > 0)
            {
                existingEmployee.Certifications.Clear();  // Limpiar las antiguas certificaciones
                foreach (var certId in command.CertificationIds)
                {
                    var certification = await _certificationRepository.GetByIdAsync(certId);
                    if (certification != null)
                    {
                        existingEmployee.Certifications.Add(certification);
                    }
                }
            }

            if (command.SpecialtyIds != null && command.SpecialtyIds.Count > 0)
            {
                existingEmployee.Specialties.Clear();  // Limpiar las antiguas especialidades
                foreach (var specId in command.SpecialtyIds)
                {
                    var specialty = await _specialtyRepository.GetByIdAsync(specId);
                    if (specialty != null)
                    {
                        existingEmployee.Specialties.Add(specialty);
                    }
                }
            }

            await _employeeRepository.UpdateAsync(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return existingEmployee;
        }

        // Manejo de la eliminación de un empleado
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
