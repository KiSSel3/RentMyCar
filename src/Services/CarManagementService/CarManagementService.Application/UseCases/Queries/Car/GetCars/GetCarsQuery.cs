using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.Models.Results;
using CarManagementService.Domain.Data.Enums;
using MediatR;

namespace CarManagementService.Application.UseCases.Queries.Car.GetCars;

public class GetCarsQuery : IRequest<PaginatedResult<CarDTO>>
{
    public Guid? ModelId { get; set; }
    public CarBodyType? BodyType { get; set; }
    public CarDriveType? DriveType { get; set; }
    public CarTransmissionType? TransmissionType { get; set; }
    public DateTime? Year { get; set; }
    public DateTime? MinYear { get; set; }
    public DateTime? MaxYear { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}