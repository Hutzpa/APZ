using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class Requests1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RecipientId",
                table: "Requests",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SenderId",
                table: "Requests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RecipientId",
                table: "Requests",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RecipientId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RecipientId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_SenderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Requests");
        }
    }
}
