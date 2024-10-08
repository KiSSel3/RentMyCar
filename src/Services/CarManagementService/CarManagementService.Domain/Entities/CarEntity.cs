using CarManagementService.Domain.Enums;

namespace CarManagementService.Domain.Entities;

public class CarEntity : BaseEntity
{
    public Guid ModelId { get; set; }
    
    public CarBodyType BodyType { get; set; }
    public CarDriveType DriveType { get; set; }
    public CarTransmissionType TransmissionType { get; set; }
    
    public DateTime ModelYear { get; set; }
    public byte[] Image { get; set; }
    
    public CarModelEntity CarModel { get; set; }
}