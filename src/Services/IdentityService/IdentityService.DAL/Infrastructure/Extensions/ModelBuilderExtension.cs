using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void SeedRolesData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity() {Id = new Guid("4DFA5285-8884-4C08-B83D-BAC2B36563D5"), Name = "Admin", NormalizedName = "ADMIN"},
            new RoleEntity() {Id = new Guid("96B72F66-4305-4635-A30C-A436DC7A0FB5"), Name = "User", NormalizedName = "USER"}
        );
    }
    
    public static void SeedUsersData(this ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher<UserEntity>();
        
        var admin = new UserEntity
        {
            Id = new Guid("9AB61044-85F6-4C5E-A93A-D860EFAE0CCE"),
            FirstName = "Admin",
            LastName = "Admin",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "admin.rent.my.car@gmail.com",
            NormalizedEmail = "ADMIN.RENT.MY.CAR@GMAIL.COM",
            PhoneNumber = "+111111111111",
            RefreshToken = "",
            RefreshTokenExpiryTime = DateTime.MinValue
        };
        
        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin");
        
        modelBuilder.Entity<UserEntity>().HasData(admin);
    }
    
    public static void SeedUsersRolesData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = new Guid("4DFA5285-8884-4C08-B83D-BAC2B36563D5"),
                UserId = new Guid("9AB61044-85F6-4C5E-A93A-D860EFAE0CCE")
            }
        );
    }
}