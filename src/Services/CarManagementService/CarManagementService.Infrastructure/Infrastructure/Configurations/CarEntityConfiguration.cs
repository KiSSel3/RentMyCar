using CarManagementService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManagementService.Infrastructure.Infrastructure.Configurations;

public class CarEntityConfiguration : BaseEntityConfiguration<CarEntity>
{
    public override void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(c => c.ModelId).IsRequired();
        builder.Property(c => c.BodyType).HasConversion<string>();
        builder.Property(c => c.DriveType).HasConversion<string>();
        builder.Property(c => c.TransmissionType).HasConversion<string>();
        builder.Property(c => c.ModelYear).IsRequired();
        builder.Property(c => c.Image).HasColumnType("bytea");

        builder.HasOne(c => c.CarModel)
            .WithMany()
            .HasForeignKey(c => c.ModelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}