namespace CarManagementService.Domain.Data.Entities;

public class ReviewEntity : BaseEntity
{
    public Guid ReviewerId { get; set; }
    public Guid RentOfferId { get; set; }
    
    public int Rating { get; set; }
    public string Comment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public RentOfferEntity RentOffer { get; set; }
}