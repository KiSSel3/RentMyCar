using BookingService.BLL.Models.DTOs.Event;

namespace BookingService.BLL.Models.DTOs.Booking;

public class BookingDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RentOfferId { get; set; }
    
    public DateTime RentalStart { get; set; }
    public DateTime RentalEnd { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public ICollection<EventDTO> Events { get; set; }
}