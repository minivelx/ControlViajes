using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class EdicionViajes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Usuarios_IdCoductor",
                table: "Viajes");

            migrationBuilder.RenameColumn(
                name: "IdCoductor",
                table: "Viajes",
                newName: "IdConductor");

            migrationBuilder.RenameIndex(
                name: "IX_Viajes_IdCoductor",
                table: "Viajes",
                newName: "IX_Viajes_IdConductor");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Usuarios_IdConductor",
                table: "Viajes",
                column: "IdConductor",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Usuarios_IdConductor",
                table: "Viajes");

            migrationBuilder.RenameColumn(
                name: "IdConductor",
                table: "Viajes",
                newName: "IdCoductor");

            migrationBuilder.RenameIndex(
                name: "IX_Viajes_IdConductor",
                table: "Viajes",
                newName: "IX_Viajes_IdCoductor");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Usuarios_IdCoductor",
                table: "Viajes",
                column: "IdCoductor",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
