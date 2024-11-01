using BookingService.Domain.Enums;

namespace BookingService.BLL.Models.DTOs.Event;

public class EventDTO
{
    public BookingStatus Status { get; set; }
    public DateTime Timestamp { get; set; }
    
    public string? Message { get; set; }
}