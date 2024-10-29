using CarManagementService.Presentation.Models.DTOs.Common;

namespace CarManagementService.Presentation.Models.DTOs.RentOffer;

public class RentOfferParametersRequestDTO : PaginationRequestDTO
{
    public Guid? CarId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableTo { get; set; }
}