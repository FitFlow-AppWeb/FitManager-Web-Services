// Employees/Application/Internal/CommandServices/ISpecialtyCommandService.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Model.Commands; 

namespace FitManager_Web_Services.Employees.Application.Internal.CommandServices
{
    public interface ISpecialtyCommandService
    {
        Task<Specialty?> Handle(CreateSpecialtyCommand command);
        Task<bool> Handle(DeleteSpecialtyCommand command);
    }
}