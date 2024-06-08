using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authorization.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedMessagesIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e0a63e1-4309-4497-afdd-eba69f2fd7b2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c20b4fa1-dee7-41d1-9654-2931426f40a9"));

            migrationBuilder.AddColumn<string>(
                name: "MessagesIds",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6db892be-aaba-4984-8257-be524e352621"), null, "User", "USER" },
                    { new Guid("c6e2d9bc-ef0a-4f70-82aa-65ba4602cbab"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6db892be-aaba-4984-8257-be524e352621"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6e2d9bc-ef0a-4f70-82aa-65ba4602cbab"));

            migrationBuilder.DropColumn(
                name: "MessagesIds",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9e0a63e1-4309-4497-afdd-eba69f2fd7b2"), null, "User", "USER" },
                    { new Guid("c20b4fa1-dee7-41d1-9654-2931426f40a9"), null, "Admin", "ADMIN" }
                });
        }
    }
}
