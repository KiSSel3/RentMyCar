using CarManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManagementService.Infrastructure.Infrastructure.Configurations;

public class ReviewEntityConfiguration : BaseEntityConfiguration<ReviewEntity>
{
    public override void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(r => r.ReviewerId).IsRequired();
        builder.Property(r => r.RentOfferId).IsRequired();
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).HasMaxLength(1000);
        builder.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(r => r.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(r => r.RentOffer)
            .WithMany()
            .HasForeignKey(r => r.RentOfferId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}