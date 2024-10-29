using CarManagementService.Domain.Data.Models;
using MediatR;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.UpdateRentOffer;

public class UpdateRentOfferCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public LocationModel LocationModel { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
}