using CarManagementService.Domain.Models;

namespace CarManagementService.Domain.Entities;

public class RentOfferEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    
    public Location Location { get; set; }
    
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public decimal PricePerDay { get; set; } 
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    
    public CarEntity Car { get; set; }
    public ICollection<ImageEntity> Images { get; set; }
}