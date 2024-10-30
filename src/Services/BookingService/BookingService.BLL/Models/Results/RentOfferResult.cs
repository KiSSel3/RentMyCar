namespace BookingService.BLL.Models.Results;

public class RentOfferResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    
    public decimal PricePerDay { get; set; }
    
    public bool IsAvailable { get; set; }
}