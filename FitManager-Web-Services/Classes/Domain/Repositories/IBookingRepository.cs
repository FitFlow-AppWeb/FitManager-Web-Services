using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(int id);
    Task<IEnumerable<Booking>> FindByMemberAsync(int memberId);
    Task<IEnumerable<Booking>> FindByClassAsync(int classId);
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(Booking booking);
}