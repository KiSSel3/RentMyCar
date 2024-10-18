using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedConcurrencyStampToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4dfa5285-8884-4c08-b83d-bac2b36563d5"),
                column: "ConcurrencyStamp",
                value: "1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96b72f66-4305-4635-a30c-a436dc7a0fb5"),
                column: "ConcurrencyStamp",
                value: "2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6a7fde01-fea9-4e17-8966-031daf63591f", "AQAAAAIAAYagAAAAEIk1+NKWfYSmTfwlP1mU/xBqEbwj+SZAzy8ARsQMoL+EyWSRdwfD1MauDu7MeKXtTw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4dfa5285-8884-4c08-b83d-bac2b36563d5"),
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96b72f66-4305-4635-a30c-a436dc7a0fb5"),
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "017ef892-4d84-46f3-ae17-78fb6a52e55f", "AQAAAAIAAYagAAAAENwXyYAD9wdDaFaIQonlkUtcie8QN5OC4MctWf3VSVn6zoRv3khLRkwxHxr5+uA8VA==" });
        }
    }
}
