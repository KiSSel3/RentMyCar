using CarManagementService.Domain.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Commands.RentOffer.CreateRentOffer;

public class CreateRentOfferCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    public LocationModel LocationModel { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
}