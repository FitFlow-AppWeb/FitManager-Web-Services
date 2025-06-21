// Members/Application/Internal/CommandServices/CertificationCommandService.cs
// (Modifica la implementación existente para usar DeleteAsync(int id))

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories; // Para IUnitOfWork y IBaseRepository

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    public class CertificationCommandService : ICertificationCommandService
    {
        private readonly ICertificationRepository _certificationRepository;
        private readonly IEmployeeRepository _employeeRepository; // Para validar EmployeeId
        private readonly IUnitOfWork _unitOfWork;

        public CertificationCommandService(ICertificationRepository certificationRepository, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _certificationRepository = certificationRepository;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<Certification?> Handle(CreateCertificationCommand command)
        {
            // Validar que el EmployeeId existe
            // Asegúrate de que IEmployeeRepository tiene GetByIdAsync o un método similar
            var employeeExists = await _employeeRepository.GetByIdAsync(command.EmployeeId);
            if (employeeExists == null)
            {
                return null; // O lanzar una excepción específica si prefieres
            }

            var certification = new Certification(command.Name, command.Description, command.EmployeeId);
            await _certificationRepository.AddAsync(certification);
            await _unitOfWork.CompleteAsync();
            return certification;
        }

        public async Task<bool> Handle(DeleteCertificationCommand command)
        {
            // CAMBIADO: Usar DeleteAsync(int id) directamente del repositorio
            // Primero, verifica si la entidad existe para retornar false si no se encuentra
            var exists = await _certificationRepository.GetByIdAsync(command.Id);
            if (exists == null)
            {
                return false; // La certificación no existe
            }

            await _certificationRepository.DeleteAsync(command.Id); // Llama al método DeleteAsync por ID
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}