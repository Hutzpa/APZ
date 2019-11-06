using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class FulDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Geoposition",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarType",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarcassNumber",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoType",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarryingCapacity",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CarryingCapacitySqM",
                table: "Cars",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerKm",
                table: "Cars",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "HeightCargoCompartment",
                table: "Cars",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LengthCargoCompartment",
                table: "Cars",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WidthCargoCompartment",
                table: "Cars",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CargoName",
                table: "Cargos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoType",
                table: "Cargos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeparturePoint",
                table: "Cargos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationPoint",
                table: "Cargos",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Cargos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Cargos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Cargos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Cargos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Cargos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Geoposition",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarcassNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CargoType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarryingCapacity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarryingCapacitySqM",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CostPerKm",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "HeightCargoCompartment",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LengthCargoCompartment",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WidthCargoCompartment",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CargoName",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "CargoType",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "DeparturePoint",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "DestinationPoint",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Cargos");
        }
    }
}
