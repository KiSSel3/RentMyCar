namespace BookingService.BLL.Models.DTOs.Booking;

public class CreateBookingDTO
{
    public Guid UserId { get; set; }
    public Guid RentOfferId { get; set; }
    
    public DateTime RentalStart { get; set; }
    public DateTime RentalEnd { get; set; }
    
    public string? Message { get; set; }
}