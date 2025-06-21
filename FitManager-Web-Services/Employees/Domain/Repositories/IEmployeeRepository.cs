using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee?> GetByDniAsync(int dni);
        Task<IEnumerable<Employee>> GetAllWithCertificationsAndSpecialtiesAsync();
        Task<Employee?> GetByIdWithCertificationsAndSpecialtiesAsync(int employeeId);
    }
}