using CarManagementService.Domain.Data.Models;

namespace CarManagementService.Presentation.Models.DTOs.RentOffer;

public class RentOfferRequestDTO
{
    public Guid CarId { get; set; }
    public LocationModel LocationModel { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
}