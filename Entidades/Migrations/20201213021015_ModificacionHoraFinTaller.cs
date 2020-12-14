using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entidades.Migrations
{
    public partial class ModificacionHoraFinTaller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FinTaller",
                table: "HistoricoTaller",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FinTaller",
                table: "HistoricoTaller",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
