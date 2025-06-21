// Employees/Application/Internal/CommandServices/SpecialtyCommandService.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands; 
using FitManager_Web_Services.Employees.Domain.Repositories; 
using FitManager_Web_Services.Shared.Domain.Repositories; 

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    public class SpecialtyCommandService : ISpecialtyCommandService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IEmployeeRepository _employeeRepository;  
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyCommandService(ISpecialtyRepository specialtyRepository, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _specialtyRepository = specialtyRepository;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<Specialty?> Handle(CreateSpecialtyCommand command)
        {
            
            var employeeExists = await _employeeRepository.GetByIdAsync(command.EmployeeId);
            if (employeeExists == null)
            {
                return null; 
            }

            var specialty = new Specialty(command.Name, command.Description, command.EmployeeId);
            await _specialtyRepository.AddAsync(specialty);
            await _unitOfWork.CompleteAsync();
            return specialty;
        }

        public async Task<bool> Handle(DeleteSpecialtyCommand command)
        {
            var exists = await _specialtyRepository.GetByIdAsync(command.Id);
            if (exists == null)
            {
                return false; 
            }

            await _specialtyRepository.DeleteAsync(command.Id); 
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}