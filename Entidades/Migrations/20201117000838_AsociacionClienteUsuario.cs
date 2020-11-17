using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class AsociacionClienteUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdCliente",
                table: "Usuarios",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Clientes_IdCliente",
                table: "Usuarios",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Clientes_IdCliente",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdCliente",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Usuarios");
        }
    }
}
