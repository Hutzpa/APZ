using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class Clear6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdOwnerId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_IdOwnerId",
                table: "Cars",
                column: "IdOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_IdOwnerId",
                table: "Cars",
                column: "IdOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_IdOwnerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_IdOwnerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IdOwnerId",
                table: "Cars");
        }
    }
}
