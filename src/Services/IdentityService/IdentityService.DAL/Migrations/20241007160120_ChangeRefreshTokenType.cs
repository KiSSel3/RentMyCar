using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRefreshTokenType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a22a913-a1dd-4215-a4b8-5b2cdc29e089", "AQAAAAIAAYagAAAAECzoNc7ZK2e99jtOyZfkdLHuMZIV/RzYYn9+CgZAL5NcEn9lwyS2Rz2ywdghPnBt2A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ab61044-85f6-4c5e-a93a-d860efae0cce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6a7fde01-fea9-4e17-8966-031daf63591f", "AQAAAAIAAYagAAAAEIk1+NKWfYSmTfwlP1mU/xBqEbwj+SZAzy8ARsQMoL+EyWSRdwfD1MauDu7MeKXtTw==" });
        }
    }
}
