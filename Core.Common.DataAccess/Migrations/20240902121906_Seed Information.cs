using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Common.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5e613fe6-1256-407b-aea6-b2e16bdfa954"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c12c0b3c-e650-49fe-a7df-5734dd97517e"));

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0fb4b6ba-f091-4097-ba38-dd9a113a864f"), null, "Admin", "ADMIN" },
                    { new Guid("2dd27d36-6283-4600-9707-ae57b75b048b"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fb4b6ba-f091-4097-ba38-dd9a113a864f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2dd27d36-6283-4600-9707-ae57b75b048b"));

            migrationBuilder.DropColumn(
                name: "Information",
                table: "Conversations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5e613fe6-1256-407b-aea6-b2e16bdfa954"), null, "User", "USER" },
                    { new Guid("c12c0b3c-e650-49fe-a7df-5734dd97517e"), null, "Admin", "ADMIN" }
                });
        }
    }
}
