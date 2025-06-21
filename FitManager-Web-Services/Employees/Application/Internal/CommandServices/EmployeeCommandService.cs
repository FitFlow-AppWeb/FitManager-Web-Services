using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing <see cref="Employee"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This service handles commands that modify the state of employee data, including creation,
    /// updates, and deletions. It orchestrates interactions with various repositories
    /// and the unit of work to ensure data consistency. It belongs to the Application layer in Clean Architecture,
    /// responsible for handling write use cases.
    /// </remarks>
    public class EmployeeCommandService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICertificationRepository _certificationRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeCommandService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository for <see cref="Employee"/> entities.</param>
        /// <param name="certificationRepository">The repository for <see cref="Certification"/> entities (though not directly used in provided methods).</param>
        /// <param name="specialtyRepository">The repository for <see cref="Specialty"/> entities (though not directly used in provided methods).</param>
        /// <param name="unitOfWork">The unit of work to manage transactional operations.</param>
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
        /// Asynchronously handles the creation of a new employee based on the provided command.
        /// </summary>
        /// <param name="command">The <see cref="CreateEmployeeCommand"/> containing the data for the new employee.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the newly created <see cref="Employee"/> object if successful, otherwise <c>null</c>.
        /// </returns>
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
            
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return employee;
        }

        /// <summary>
        /// Asynchronously handles the update of an existing employee based on the provided command.
        /// </summary>
        /// <param name="command">The <see cref="UpdateEmployeeCommand"/> containing the updated data for the employee.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the updated <see cref="Employee"/> object if found and updated, otherwise <c>null</c>.
        /// </returns>
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
            
            await _employeeRepository.UpdateAsync(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return existingEmployee;
        }

        /// <summary>
        /// Asynchronously handles the deletion of an employee based on the provided command.
        /// </summary>
        /// <param name="command">The <see cref="DeleteEmployeeCommand"/> containing the ID of the employee to delete.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if the employee was successfully deleted; otherwise, <c>false</c> if the employee was not found.
        /// </returns>
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