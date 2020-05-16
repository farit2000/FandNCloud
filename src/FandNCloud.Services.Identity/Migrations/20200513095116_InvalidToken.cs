using Microsoft.EntityFrameworkCore.Migrations;

namespace FandNCloud.Services.Identity.Migrations
{
    public partial class InvalidToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvalidToken_AspNetUsers_UserId",
                table: "InvalidToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvalidToken",
                table: "InvalidToken");

            migrationBuilder.RenameTable(
                name: "InvalidToken",
                newName: "InvalidTokens");

            migrationBuilder.RenameIndex(
                name: "IX_InvalidToken_UserId",
                table: "InvalidTokens",
                newName: "IX_InvalidTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvalidTokens",
                table: "InvalidTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvalidTokens_AspNetUsers_UserId",
                table: "InvalidTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvalidTokens_AspNetUsers_UserId",
                table: "InvalidTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvalidTokens",
                table: "InvalidTokens");

            migrationBuilder.RenameTable(
                name: "InvalidTokens",
                newName: "InvalidToken");

            migrationBuilder.RenameIndex(
                name: "IX_InvalidTokens_UserId",
                table: "InvalidToken",
                newName: "IX_InvalidToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvalidToken",
                table: "InvalidToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvalidToken_AspNetUsers_UserId",
                table: "InvalidToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
