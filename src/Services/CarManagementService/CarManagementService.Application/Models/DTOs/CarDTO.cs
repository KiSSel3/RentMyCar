using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Enums;

namespace CarManagementService.Application.Models.DTOs;

public class CarDTO
{
    public Guid Id { get; set; }
    public CarBodyType BodyType { get; set; }
    public CarDriveType DriveType { get; set; }
    public CarTransmissionType TransmissionType { get; set; }
    public DateTime ModelYear { get; set; }
    public string Image { get; set; }
    public CarModelDTO CarModel { get; set; }
}