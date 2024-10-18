using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SetNormalizedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4dfa5285-8884-4c08-b83d-bac2b36563d5"),
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96b72f66-4305-4635-a30c-a436dc7a0fb5"),
                column: "NormalizedName",
                value: "USER");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "017ef892-4d84-46f3-ae17-78fb6a52e55f", "ADMIN.RENT.MY.CAR@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENwXyYAD9wdDaFaIQonlkUtcie8QN5OC4MctWf3VSVn6zoRv3khLRkwxHxr5+uA8VA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4dfa5285-8884-4c08-b83d-bac2b36563d5"),
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96b72f66-4305-4635-a30c-a436dc7a0fb5"),
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "ac0c173b-f91e-4af6-a0cd-d87a1e4421d9", null, null, "AQAAAAIAAYagAAAAEJGYwbQaSYp42mUYmYvlD0NLu4hHlIEtI6Efm+AlVmmDH2JsfGvv5LOOP4eyXKf9pg==" });
        }
    }
}
