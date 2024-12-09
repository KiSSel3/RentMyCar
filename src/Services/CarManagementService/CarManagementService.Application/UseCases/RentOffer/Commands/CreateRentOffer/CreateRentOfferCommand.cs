using CarManagementService.Domain.Data.Models;
using MediatR;

namespace CarManagementService.Application.UseCases.RentOffer.Commands.CreateRentOffer;

public class CreateRentOfferCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    public LocationModel LocationModel { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
}