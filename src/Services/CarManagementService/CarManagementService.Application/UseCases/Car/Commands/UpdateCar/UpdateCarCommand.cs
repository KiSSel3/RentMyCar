using CarManagementService.Domain.Data.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarManagementService.Application.UseCases.Car.Commands.UpdateCar;

public class UpdateCarCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public CarBodyType BodyType { get; set; }
    public CarDriveType DriveType { get; set; }
    public CarTransmissionType TransmissionType { get; set; }
    public DateTime ModelYear { get; set; }
    public IFormFile? Image { get; set; }
}