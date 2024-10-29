using CarManagementService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManagementService.Infrastructure.Infrastructure.Configurations;

public class CarModelEntityConfiguration : BaseEntityConfiguration<CarModelEntity>
{
    public override void Configure(EntityTypeBuilder<CarModelEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(cm => cm.CarBrandId).IsRequired();
        builder.Property(cm => cm.Name).IsRequired().HasMaxLength(100);

        builder.HasOne(cm => cm.Brand)
            .WithMany()
            .HasForeignKey(cm => cm.CarBrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}