using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Repositories;

public interface IAttendanceRepository
{
    Task<Attendance?> GetByIdAsync(int id);
    Task<IEnumerable<Attendance>> FindByClassAsync(int classId);
    Task<IEnumerable<Attendance>> FindByMemberAsync(int memberId);
    Task AddAsync(Attendance attendance);
    Task UpdateAsync(Attendance attendance);
    Task DeleteAsync(Attendance attendance);
}