using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class refConflictFixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoInCars_Cargos_CargoId_Cargo",
                table: "CargoInCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoInCars_Cargos_CargoId_Cargo",
                table: "CargoInCars",
                column: "CargoId_Cargo",
                principalTable: "Cargos",
                principalColumn: "Id_Cargo",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_CargoInCars_Cargos_CargoId_Cargo",
                table: "CargoInCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoInCars_Cargos_CargoId_Cargo",
                table: "CargoInCars",
                column: "CargoId_Cargo",
                principalTable: "Cargos",
                principalColumn: "Id_Cargo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
