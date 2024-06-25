using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Common.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedasoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_OwnerId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_OwnerId",
                table: "Conversations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("42ff7215-0727-43c2-bde8-c005c8560a16"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b84354e9-cfcb-4a4c-8b5c-820d7edec2f5"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ConversationId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Messages",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Conversations",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0cbd8a81-b480-4ce5-8671-566dc9ab5c58"), null, "Admin", "ADMIN" },
                    { new Guid("2395064b-b04c-40f8-b8e5-da0ce9a7e79e"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Messages",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Conversations",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConversationId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("42ff7215-0727-43c2-bde8-c005c8560a16"), null, "Admin", "ADMIN" },
                    { new Guid("b84354e9-cfcb-4a4c-8b5c-820d7edec2f5"), null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_OwnerId",
                table: "Conversations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_OwnerId",
                table: "Conversations",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id");
        }
    }
}
