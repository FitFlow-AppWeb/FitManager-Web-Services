using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    public interface ISpecialtyRepository : IBaseRepository<Specialty>
    {
        Task<IEnumerable<Specialty>> GetByIdsAsync(IEnumerable<int> ids); // Agregar este método
    }
}