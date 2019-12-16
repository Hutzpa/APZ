using Microsoft.EntityFrameworkCore.Migrations;

namespace CargoWorld.Migrations
{
    public partial class refConflictFixing1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_IdOwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_IdOwnerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RecipId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_IdOwnerId",
                table: "Cars",
                column: "IdOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_IdOwnerId",
                table: "Groups",
                column: "IdOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RecipId",
                table: "Requests",
                column: "RecipId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_IdOwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_IdOwnerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RecipId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_Id_OwnerId",
                table: "Cargos",
                column: "Id_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_IdOwnerId",
                table: "Cars",
                column: "IdOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_IdOwnerId",
                table: "Groups",
                column: "IdOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RecipId",
                table: "Requests",
                column: "RecipId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
