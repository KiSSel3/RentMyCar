using CarManagementService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManagementService.Infrastructure.Infrastructure.Configurations;

public class RentOfferEntityConfiguration : BaseEntityConfiguration<RentOfferEntity>
{
    public override void Configure(EntityTypeBuilder<RentOfferEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(ro => ro.UserId).IsRequired();
        builder.Property(ro => ro.CarId).IsRequired();
        builder.Property(ro => ro.AvailableFrom).IsRequired();
        builder.Property(ro => ro.AvailableTo).IsRequired();
        builder.Property(ro => ro.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(ro => ro.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(ro => ro.PricePerDay).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(ro => ro.Description).HasMaxLength(1000);
        builder.Property(ro => ro.IsAvailable).IsRequired();

        builder.OwnsOne(ro => ro.LocationModel, lm =>
        {
            lm.Property(l => l.City).IsRequired().HasMaxLength(100);
            lm.Property(l => l.Street).IsRequired().HasMaxLength(200);
            lm.Property(l => l.Building).IsRequired().HasMaxLength(50);
        });

        builder.HasOne(ro => ro.Car)
            .WithMany()
            .HasForeignKey(ro => ro.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ro => ro.Images)
            .WithOne()
            .HasForeignKey(i => i.RentOfferId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}