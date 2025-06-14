using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    public interface ICertificationRepository
    {
        Task<IEnumerable<Certification>> GetAllAsync();  // Esta función debe estar aquí
        Task<Certification?> GetByIdAsync(int id);
        Task AddAsync(Certification certification);
    }
}