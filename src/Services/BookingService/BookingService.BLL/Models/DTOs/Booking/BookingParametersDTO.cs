using BookingService.Domain.Enums;

namespace BookingService.BLL.Models.DTOs.Booking;

public class BookingParametersDTO
{
    public Guid? UserId { get; set; }
    public Guid? RentOfferId { get; set; }
    
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public BookingStatus? Status { get; set; }
}