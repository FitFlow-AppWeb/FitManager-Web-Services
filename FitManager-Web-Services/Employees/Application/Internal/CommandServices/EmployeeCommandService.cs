using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

using System.Threading.Tasks;
using System.Collections.Generic; 

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing employee operations.
    /// This service orchestrates business logic related to creating, updating, and deleting employee records.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling employee-specific use cases.
    /// </summary>
    public class EmployeeCommandService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICertificationRepository _certificationRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeCommandService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository for managing employee data.</param>
        /// <param name="certificationRepository">The certification repository for accessing certification data.</param>
        /// <param name="specialtyRepository">The specialty repository for accessing specialty data.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions and persistence.</param>
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

        /// <summary>
        /// Handles the creation of a new employee based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method retrieves necessary certification and specialty entities,
        /// creates a new <see cref="Employee"/> aggregate, assigns its related entities,
        /// and persists the new employee to the database, completing the unit of work.
        /// </remarks>
        /// <param name="command">The command containing the data for the new employee.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the created <see cref="Employee"/> aggregate if successful, otherwise null
        /// if required certifications or specialties are not found.</returns>
        public async Task<Employee?> Handle(CreateEmployeeCommand command)
        {
            var certifications = await _certificationRepository.GetByIdsAsync(command.CertificationIds);
            var specialties = await _specialtyRepository.GetByIdsAsync(command.SpecialtyIds);

            if (certifications == null || specialties == null)
            {
                return null; 
            }

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

            employee.AssignCertifications(certifications);
            employee.AssignSpecialties(specialties);

            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return employee;
        }

        /// <summary>
        /// Handles the update of an existing employee based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method retrieves an existing <see cref="Employee"/> aggregate,
        /// updates its core properties including password, wage, role, and work hours.
        /// If new certification or specialty IDs are provided, it fetches and re-assigns them.
        /// Finally, it persists all changes to the database.
        /// </remarks>
        /// <param name="command">The command containing the data for the employee update.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the updated <see cref="Employee"/> aggregate if found and updated, otherwise null.</returns>
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
                command.Email,
                command.Password,
                command.Wage,
                command.Role,
                command.WorkHours
            );

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

            await _employeeRepository.UpdateAsync(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return existingEmployee;
        }

        /// <summary>
        /// Handles the deletion of an existing employee based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method first retrieves the <see cref="Employee"/> aggregate by its ID
        /// to ensure it exists, then deletes it using the employee repository,
        /// and finally completes the unit of work.
        /// </remarks>
        /// <param name="command">The command containing the ID of the employee to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// is true if the employee was deleted successfully, false otherwise (e.g., if not found).</returns>
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