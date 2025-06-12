using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Members.Domain.Repositories
{
    public interface IMembershipTypeRepository
    {
        Task<IEnumerable<MembershipType>> GetAllAsync();
        Task<MembershipType?> GetByIdAsync(int id);
    }
}