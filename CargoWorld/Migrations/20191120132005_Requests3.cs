using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class Requests3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCar",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCargo",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGroup",
                table: "Requests",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdDriver",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCar",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IdCargo",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IdGroup",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "IdDriver",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
