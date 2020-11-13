using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class AjusteSedeCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sedes_Clientes_ClienteId",
                table: "Sedes");

            migrationBuilder.DropTable(
                name: "SedesXClientes");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Sedes",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Sedes_ClienteId",
                table: "Sedes",
                newName: "IX_Sedes_IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Sedes_Clientes_IdCliente",
                table: "Sedes",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sedes_Clientes_IdCliente",
                table: "Sedes");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Sedes",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Sedes_IdCliente",
                table: "Sedes",
                newName: "IX_Sedes_ClienteId");

            migrationBuilder.CreateTable(
                name: "SedesXClientes",
                columns: table => new
                {
                    IdSede = table.Column<int>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SedesXClientes", x => new { x.IdSede, x.IdCliente });
                    table.UniqueConstraint("AK_SedesXClientes_IdCliente_IdSede", x => new { x.IdCliente, x.IdSede });
                    table.ForeignKey(
                        name: "FK_SedesXClientes_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SedesXClientes_Sedes_IdSede",
                        column: x => x.IdSede,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sedes_Clientes_ClienteId",
                table: "Sedes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
