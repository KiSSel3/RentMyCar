using CarManagementService.Presentation.Models.DTOs.Common;

namespace CarManagementService.Presentation.Models.DTOs.Review;

public class ReviewParametersRequestDTO : PaginationRequestDTO
{
    public Guid? ReviewerId { get; set; }
    public Guid? RentOfferId { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
    public int? MinRating { get; set; }
}