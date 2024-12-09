using BookingService.Domain.Enums;

namespace BookingService.BLL.Models.DTOs.Booking;

public class BookingParametersDTO
{
    public Guid? UserId { get; set; }
    public Guid? RentOfferId { get; set; }
    
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    
    public DateTime? EndDateFrom { get; set; }
    public DateTime? EndDateTo { get; set; }

    public BookingStatus? Status { get; set; }
}