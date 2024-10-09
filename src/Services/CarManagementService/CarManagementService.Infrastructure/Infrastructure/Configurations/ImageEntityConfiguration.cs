using CarManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManagementService.Infrastructure.Infrastructure.Configurations;

public class ImageEntityConfiguration : BaseEntityConfiguration<ImageEntity>
{
    public override void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.RentOfferId).IsRequired();
        builder.Property(i => i.Image).IsRequired().HasColumnType("bytea");
    }
}