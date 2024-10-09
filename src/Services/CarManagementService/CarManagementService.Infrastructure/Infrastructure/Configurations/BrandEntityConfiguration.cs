using CarManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManagementService.Infrastructure.Infrastructure.Configurations;

public class BrandEntityConfiguration : BaseEntityConfiguration<BrandEntity>
{
    public override void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
    }
}