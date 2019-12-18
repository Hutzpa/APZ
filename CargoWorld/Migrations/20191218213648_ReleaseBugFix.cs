using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class ReleaseBugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoInCars_Cars_TransporterIdCar",
                table: "CargoInCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Groups_IdGroup1",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoInCars_Cars_TransporterIdCar",
                table: "CargoInCars",
                column: "TransporterIdCar",
                principalTable: "Cars",
                principalColumn: "IdCar",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Groups_IdGroup1",
                table: "Cars",
                column: "IdGroup1",
                principalTable: "Groups",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoInCars_Cars_TransporterIdCar",
                table: "CargoInCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Groups_IdGroup1",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoInCars_Cars_TransporterIdCar",
                table: "CargoInCars",
                column: "TransporterIdCar",
                principalTable: "Cars",
                principalColumn: "IdCar",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Groups_IdGroup1",
                table: "Cars",
                column: "IdGroup1",
                principalTable: "Groups",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
