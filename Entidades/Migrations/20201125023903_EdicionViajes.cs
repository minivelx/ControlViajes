using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class EdicionViajes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdAuxiliar",
                table: "Viajes",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_IdAuxiliar",
                table: "Viajes",
                column: "IdAuxiliar");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Usuarios_IdAuxiliar",
                table: "Viajes",
                column: "IdAuxiliar",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Usuarios_IdAuxiliar",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_IdAuxiliar",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "IdAuxiliar",
                table: "Viajes");
        }
    }
}
