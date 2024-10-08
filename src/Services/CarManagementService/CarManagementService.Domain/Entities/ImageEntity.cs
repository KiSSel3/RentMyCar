namespace CarManagementService.Domain.Entities;

public class ImageEntity : BaseEntity
{
    public Guid RentOfferId { get; set; }
    
    public byte[] Image { get; set; }
    
    public RentOfferEntity RentOffer { get; set; }
}