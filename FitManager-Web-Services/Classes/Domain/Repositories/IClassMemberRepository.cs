using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Repositories;

public interface IClassMemberRepository
{
    Task AddAsync(ClassMember classMember);
    Task RemoveAsync(int memberId, int classId);
    Task<bool> ExistsAsync(int memberId, int classId);
    Task<ClassMember?> FindByIdsAsync(int memberId, int classId);
}