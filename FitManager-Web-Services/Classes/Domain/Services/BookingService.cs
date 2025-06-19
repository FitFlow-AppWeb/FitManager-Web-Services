using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Booking> CreateBookingAsync(int memberId, int classId, DateTime date)
    {
        var booking = new Booking(memberId, classId, date);
        await _bookingRepository.AddAsync(booking);
        return booking;
    }

    public async Task CancelBookingAsync(int bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new Exception("Booking not found");
        
        await _bookingRepository.DeleteAsync(booking);
    }

    public async Task<IEnumerable<Booking>> GetBookingsByMemberAsync(int memberId) => 
        await _bookingRepository.FindByMemberAsync(memberId);

    public async Task<IEnumerable<Booking>> GetBookingsByClassAsync(int classId) => 
        await _bookingRepository.FindByClassAsync(classId);
    
    public async Task<Booking?> GetBookingByIdAsync(int id)
    {
        return await _bookingRepository.GetByIdAsync(id);
    }
}