namespace CarManagementService.Application.Models.DTOs;

public class ReviewDTO
{
    public Guid Id { get; set; }
    public Guid ReviewerId { get; set; }
    public Guid RentOfferId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}