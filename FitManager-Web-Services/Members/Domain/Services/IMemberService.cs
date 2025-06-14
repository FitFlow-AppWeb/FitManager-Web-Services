
using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Members.Domain.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int id);
        Task<Member> GetByDniAsync(int dni);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int id);
    }
}