using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authorization.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9e0a63e1-4309-4497-afdd-eba69f2fd7b2"), null, "User", "USER" },
                    { new Guid("c20b4fa1-dee7-41d1-9654-2931426f40a9"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e0a63e1-4309-4497-afdd-eba69f2fd7b2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c20b4fa1-dee7-41d1-9654-2931426f40a9"));
        }
    }
}
