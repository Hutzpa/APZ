using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class Groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cargos_CargoId_Cargo",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupIdGroup",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "_Users");

            migrationBuilder.RenameIndex(
                name: "IX_Users_GroupIdGroup",
                table: "_Users",
                newName: "IX__Users_GroupIdGroup");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CargoId_Cargo",
                table: "_Users",
                newName: "IX__Users_CargoId_Cargo");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Users",
                table: "_Users",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK__Users_Cargos_CargoId_Cargo",
                table: "_Users",
                column: "CargoId_Cargo",
                principalTable: "Cargos",
                principalColumn: "Id_Cargo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Users_Groups_GroupIdGroup",
                table: "_Users",
                column: "GroupIdGroup",
                principalTable: "Groups",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Users_Cargos_CargoId_Cargo",
                table: "_Users");

            migrationBuilder.DropForeignKey(
                name: "FK__Users_Groups_GroupIdGroup",
                table: "_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Users",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "_Users",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX__Users_GroupIdGroup",
                table: "Users",
                newName: "IX_Users_GroupIdGroup");

            migrationBuilder.RenameIndex(
                name: "IX__Users_CargoId_Cargo",
                table: "Users",
                newName: "IX_Users_CargoId_Cargo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cargos_CargoId_Cargo",
                table: "Users",
                column: "CargoId_Cargo",
                principalTable: "Cargos",
                principalColumn: "Id_Cargo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupIdGroup",
                table: "Users",
                column: "GroupIdGroup",
                principalTable: "Groups",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
