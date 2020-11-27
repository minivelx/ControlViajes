using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class EdicionViajes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Usuarios_IdAuxiliar",
                table: "Viajes");

            migrationBuilder.AlterColumn<string>(
                name: "IdAuxiliar",
                table: "Viajes",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

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

            migrationBuilder.AlterColumn<string>(
                name: "IdAuxiliar",
                table: "Viajes",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Usuarios_IdAuxiliar",
                table: "Viajes",
                column: "IdAuxiliar",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
