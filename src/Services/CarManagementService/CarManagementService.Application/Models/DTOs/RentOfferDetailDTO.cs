namespace CarManagementService.Application.Models.DTOs;

public class RentOfferDetailDTO : RentOfferDTO
{
    public CarDTO Car { get; set; }
    public List<ImageDTO> Images { get; set; }
}