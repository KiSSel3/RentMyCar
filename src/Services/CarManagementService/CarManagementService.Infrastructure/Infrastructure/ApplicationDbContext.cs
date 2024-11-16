using System.Reflection;
using CarManagementService.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<CarModelEntity> CarModels { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<RentOfferEntity> RentOffers { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}