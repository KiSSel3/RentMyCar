using BookingService.Domain.Enums;

namespace BookingService.Domain.Entities;

public class EventEntity
{
    public BookingStatus Status { get; set; }
    public DateTime Timestamp { get; set; }
    
    public string? Message { get; set; }
}