using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Services;

public interface IClassService
{
    Task<Class> CreateClassAsync(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId);
    Task UpdateClassAsync(int id, string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId);
    Task DeleteClassAsync(int id);
    Task<Class?> GetClassByIdAsync(int id);
    Task<IEnumerable<Class>> GetAllClassesAsync();
    Task<IEnumerable<Class>> GetClassesByTypeAsync(string type);
    Task<IEnumerable<ClassMember>> GetClassMembersAsync(int classId);
}