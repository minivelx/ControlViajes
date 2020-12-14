using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class HistoricoTaller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstadoTaller",
                table: "Camiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinTaller",
                table: "Camiones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InicioTaller",
                table: "Camiones",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricoTaller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCamion = table.Column<int>(nullable: false),
                    Placa = table.Column<string>(maxLength: 7, nullable: false),
                    InicioTaller = table.Column<DateTime>(nullable: false),
                    FinTaller = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTaller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoTaller_Camiones_IdCamion",
                        column: x => x.IdCamion,
                        principalTable: "Camiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camiones_Placa",
                table: "Camiones",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTaller_IdCamion",
                table: "HistoricoTaller",
                column: "IdCamion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoTaller");

            migrationBuilder.DropIndex(
                name: "IX_Camiones_Placa",
                table: "Camiones");

            migrationBuilder.DropColumn(
                name: "EstadoTaller",
                table: "Camiones");

            migrationBuilder.DropColumn(
                name: "FinTaller",
                table: "Camiones");

            migrationBuilder.DropColumn(
                name: "InicioTaller",
                table: "Camiones");
        }
    }
}
