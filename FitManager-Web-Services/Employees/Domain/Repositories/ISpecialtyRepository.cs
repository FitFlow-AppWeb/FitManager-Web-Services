using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();  // Esta función debe estar aquí
        Task<Specialty?> GetByIdAsync(int id);
        Task AddAsync(Specialty specialty);
    }
}