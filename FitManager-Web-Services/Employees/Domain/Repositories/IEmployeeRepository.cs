using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByDniAsync(int dni);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}