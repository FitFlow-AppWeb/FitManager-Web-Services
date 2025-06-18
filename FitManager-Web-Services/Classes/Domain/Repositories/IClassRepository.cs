using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Repositories;

public interface IClassRepository
{
    Task<Class?> GetByIdAsync(int id);
    Task<IEnumerable<Class>> ListAsync();
    Task<IEnumerable<Class>> FindByTypeAsync(string type);
    Task AddAsync(Class fitnessClass);
    Task UpdateAsync(Class fitnessClass);
    Task DeleteAsync(Class fitnessClass);
    

    Task<IEnumerable<ClassMember>> GetMembersByClassIdAsync(int classId);
    Task<IEnumerable<Booking>> GetBookingsByClassIdAsync(int classId);
}