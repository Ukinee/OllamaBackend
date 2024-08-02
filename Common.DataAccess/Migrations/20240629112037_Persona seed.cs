using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Common.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Personaseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0cbd8a81-b480-4ce5-8671-566dc9ab5c58"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2395064b-b04c-40f8-b8e5-da0ce9a7e79e"));

            migrationBuilder.DropColumn(
                name: "ChatName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatRole",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PersonaLinks",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaLinks", x => new { x.UserId, x.ConversationId });
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("52c4a56f-439e-4ca7-96dc-811f7990284e"), null, "Admin", "ADMIN" },
                    { new Guid("94dec28d-1751-420a-a44b-fc998630329e"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonaLinks");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52c4a56f-439e-4ca7-96dc-811f7990284e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("94dec28d-1751-420a-a44b-fc998630329e"));

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "Messages");

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
                    { new Guid("0cbd8a81-b480-4ce5-8671-566dc9ab5c58"), null, "Admin", "ADMIN" },
                    { new Guid("2395064b-b04c-40f8-b8e5-da0ce9a7e79e"), null, "User", "USER" }
                });
        }
    }
}
