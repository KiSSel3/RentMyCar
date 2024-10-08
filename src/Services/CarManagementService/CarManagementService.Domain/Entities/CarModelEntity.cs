namespace CarManagementService.Domain.Entities;

public class CarModelEntity : BaseEntity
{
    public Guid CarBrandId { get; set; }
    
    public string Name { get; set; }
    
    public BrandEntity Brand { get; set; }
}