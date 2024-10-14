namespace CarManagementService.Domain.Data.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}