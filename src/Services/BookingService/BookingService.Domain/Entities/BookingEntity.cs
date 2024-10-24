namespace BookingService.Domain.Entities;

public class BookingEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RentOfferId { get; set; }
    
    public DateTime RentalStart { get; set; }
    public DateTime RentalEnd { get; set; }
    
    public decimal TotalPrice { get; set; }

    public ICollection<EventEntity> Events { get; set; } 
}