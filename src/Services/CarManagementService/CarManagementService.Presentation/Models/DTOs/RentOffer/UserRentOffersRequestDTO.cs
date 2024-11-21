using CarManagementService.Presentation.Models.DTOs.Common;

namespace CarManagementService.Presentation.Models.DTOs.RentOffer;

public class UserRentOffersRequestDTO : PaginationRequestDTO
{
    public bool? IsAvailable { get; set; }
}