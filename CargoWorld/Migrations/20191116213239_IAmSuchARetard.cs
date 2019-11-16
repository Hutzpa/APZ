using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class IAmSuchARetard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cargos_CargoId_Cargo",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupIdGroup",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoInCars_Cargos_Id_Cargo1",
                table: "CargoInCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cars_CarIdCar",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CarIdCar",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_CargoInCars_Id_Cargo1",
                table: "CargoInCars");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CargoId_Cargo",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupIdGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarIdCar",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Id_Cargo1",
                table: "CargoInCars");

            migrationBuilder.DropColumn(
                name: "CargoId_Cargo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupIdGroup",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdOwnerId",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGroup1",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id_OwnerId",
                table: "Cargos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferId_Delivery",
                table: "Cargos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_IdOwnerId",
                table: "Groups",
                column: "IdOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_IdGroup1",
                table: "Cars",
                column: "IdGroup1");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_TransferId_Delivery",
                table: "Cargos",
                column: "TransferId_Delivery");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_CargoInCars_TransferId_Delivery",
                table: "Cargos",
                column: "TransferId_Delivery",
                principalTable: "CargoInCars",
                principalColumn: "Id_Delivery",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Groups_IdGroup1",
                table: "Cars",
                column: "IdGroup1",
                principalTable: "Groups",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_IdOwnerId",
                table: "Groups",
                column: "IdOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_CargoInCars_TransferId_Delivery",
                table: "Cargos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Groups_IdGroup1",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_IdOwnerId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_IdOwnerId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Cars_IdGroup1",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_TransferId_Delivery",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "IdOwnerId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IdGroup1",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Id_OwnerId",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "TransferId_Delivery",
                table: "Cargos");

            migrationBuilder.AddColumn<int>(
                name: "CarIdCar",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_Cargo1",
                table: "CargoInCars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CargoId_Cargo",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupIdGroup",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CarIdCar",
                table: "Groups",
                column: "CarIdCar");

            migrationBuilder.CreateIndex(
                name: "IX_CargoInCars_Id_Cargo1",
                table: "CargoInCars",
                column: "Id_Cargo1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CargoId_Cargo",
                table: "AspNetUsers",
                column: "CargoId_Cargo");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupIdGroup",
                table: "AspNetUsers",
                column: "GroupIdGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cargos_CargoId_Cargo",
                table: "AspNetUsers",
                column: "CargoId_Cargo",
                principalTable: "Cargos",
                principalColumn: "Id_Cargo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupIdGroup",
                table: "AspNetUsers",
                column: "GroupIdGroup",
                principalTable: "Groups",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CargoInCars_Cargos_Id_Cargo1",
                table: "CargoInCars",
                column: "Id_Cargo1",
                principalTable: "Cargos",
                principalColumn: "Id_Cargo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cars_CarIdCar",
                table: "Groups",
                column: "CarIdCar",
                principalTable: "Cars",
                principalColumn: "IdCar",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
