using BookingService.BLL.Models.DTOs.Booking;
using BookingService.BLL.Models.DTOs.Common;

namespace BookingService.BLL.Services.Interfaces;

public interface IBookingService
{
    Task CreateBookingAsync(CreateBookingDTO createBookingDTO, CancellationToken cancellationToken = default);
    
    Task UpdateBookingAsync(Guid bookingId, UpdateBookingDTO updateBookingDTO, CancellationToken cancellationToken = default);
    
    Task DeleteBookingAsync(Guid bookingId, CancellationToken cancellationToken = default);
    
    Task<BookingDTO> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<BookingDTO>> GetBookingsAsync(BookingParametersDTO parametersDTO, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<DateTime>> GetAvailableDatesAsync(Guid rentOfferId, DateRangeDTO dateRangeDTO, CancellationToken cancellationToken = default);
}