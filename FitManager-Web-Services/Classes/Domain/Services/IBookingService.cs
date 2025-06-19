using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Domain.Services;

public interface IBookingService
{
    Task<Booking> CreateBookingAsync(int memberId, int classId, DateTime date);
    Task CancelBookingAsync(int bookingId);
    Task<IEnumerable<Booking>> GetBookingsByMemberAsync(int memberId);
    Task<IEnumerable<Booking>> GetBookingsByClassAsync(int classId);
    Task<Booking?> GetBookingByIdAsync(int id);
}