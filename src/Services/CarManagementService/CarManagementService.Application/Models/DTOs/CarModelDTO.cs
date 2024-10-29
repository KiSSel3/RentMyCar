namespace CarManagementService.Application.Models.DTOs;

public class CarModelDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public BrandDTO Brand { get; set; }
}