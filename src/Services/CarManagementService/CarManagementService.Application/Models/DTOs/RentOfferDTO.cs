using CarManagementService.Domain.Data.Models;

namespace CarManagementService.Application.Models.DTOs;

public class RentOfferDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    public LocationModel Location { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
}