using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
