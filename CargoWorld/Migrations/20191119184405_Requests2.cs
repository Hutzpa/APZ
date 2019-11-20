using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class Requests2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_SenderId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SenderId",
                table: "Requests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
