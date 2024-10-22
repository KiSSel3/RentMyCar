using BookingService.Domain.Enums;

namespace BookingService.BLL.Models.DTOs.Booking;

public class UpdateBookingDTO
{
    public BookingStatus Status { get; set; }
    
    public string? Message { get; set; }
}