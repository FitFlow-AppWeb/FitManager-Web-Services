// Members/Application/Internal/CommandServices/ICertificationCommandService.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands;

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    public interface ICertificationCommandService
    {
        Task<Certification?> Handle(CreateCertificationCommand command);
        Task<bool> Handle(DeleteCertificationCommand command);
    }
}