namespace CarManagementService.Domain.Data.Entities;

public class ImageEntity : BaseEntity
{
    public Guid RentOfferId { get; set; }
    
    public byte[] Image { get; set; }
}