using CarManagementService.Domain.Data.Enums;
using CarManagementService.Presentation.Models.DTOs.Common;

namespace CarManagementService.Presentation.Models.DTOs.Car;

public class CarParametersRequestDTO : PaginationRequestDTO
{
    public Guid? ModelId { get; set; }
    public CarBodyType? BodyType { get; set; }
    public CarDriveType? DriveType { get; set; }
    public CarTransmissionType? TransmissionType { get; set; }
    public DateTime? Year { get; set; }
    public DateTime? MinYear { get; set; }
    public DateTime? MaxYear { get; set; }
}