using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Common.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Seedpersona2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52c4a56f-439e-4ca7-96dc-811f7990284e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("94dec28d-1751-420a-a44b-fc998630329e"));

            migrationBuilder.AddColumn<string>(
                name: "ChatName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChatRole",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("95c2789c-52df-4389-80a6-1b61ce6bd418"), null, "User", "USER" },
                    { new Guid("e826b374-b43b-47a0-8a2e-0e64a48844d4"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("95c2789c-52df-4389-80a6-1b61ce6bd418"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e826b374-b43b-47a0-8a2e-0e64a48844d4"));

            migrationBuilder.DropColumn(
                name: "ChatName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatRole",
                table: "Messages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("52c4a56f-439e-4ca7-96dc-811f7990284e"), null, "Admin", "ADMIN" },
                    { new Guid("94dec28d-1751-420a-a44b-fc998630329e"), null, "User", "USER" }
                });
        }
    }
}
