using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories; 
using System.Threading.Tasks; 

namespace FitManager_Web_Services.Members.Domain.Repositories
{
    public interface IMemberRepository : IBaseRepository<Member>
    {
        Task<Member?> GetByDniAsync(int dni);
    }
}