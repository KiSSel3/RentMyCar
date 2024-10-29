namespace CarManagementService.Presentation.Models.DTOs.RentOffer;

public class CreateRentOfferRequestDTO : RentOfferRequestDTO
{
    public Guid UserId { get; set; }
}