using CarManagementService.Domain.Data.Enums;

namespace CarManagementService.Presentation.Models.DTOs.Car;

public class CarRequestDTO
{
    public Guid ModelId { get; set; }
    public CarBodyType BodyType { get; set; }
    public CarDriveType DriveType { get; set; }
    public CarTransmissionType TransmissionType { get; set; }
    public DateTime ModelYear { get; set; }
    public IFormFile? Image { get; set; }
}