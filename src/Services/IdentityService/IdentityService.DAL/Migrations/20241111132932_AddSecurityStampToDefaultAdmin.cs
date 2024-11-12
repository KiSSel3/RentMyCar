using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityStampToDefaultAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e012336c-218c-4d88-9d26-2138dc7d4e79", "AQAAAAIAAYagAAAAEFtiM1MSHtY3mfLustESVL21tQHKvRcFBpxEAKaGJ0f82Z38BT8aTkfOMOiWQdFBag==", "4a6123ad-7670-4539-b4ee-3230ffec7135" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a22a913-a1dd-4215-a4b8-5b2cdc29e089", "AQAAAAIAAYagAAAAECzoNc7ZK2e99jtOyZfkdLHuMZIV/RzYYn9+CgZAL5NcEn9lwyS2Rz2ywdghPnBt2A==", null });
        }
    }
}
