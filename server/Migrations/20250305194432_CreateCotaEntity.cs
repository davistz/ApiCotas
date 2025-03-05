using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCotas.Migrations
{
    /// <inheritdoc />
    public partial class CreateCotaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cotas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConsorcioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroCota = table.Column<double>(type: "float", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotas");
        }
    }
}
