namespace CarManagementService.Presentation.Models.DTOs.RentOffer;

public class UpdateRentOfferRequestDTO : RentOfferRequestDTO
{
    public Guid Id { get; set; }
    public bool IsAvailable { get; set; }
}