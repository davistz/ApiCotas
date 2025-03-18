using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCotas.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_update = table.Column<DateTime>(type: "datetime2", nullable: false),
                    roles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Consorcios",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    numero_participantes = table.Column<int>(type: "int", nullable: false),
                    data_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_update = table.Column<DateTime>(type: "datetime2", nullable: false),
                    criador_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nome_criador = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consorcios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Consorcios_Users_criador_id",
                        column: x => x.criador_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cotas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    consorcioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    numeroCota = table.Column<double>(type: "float", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    data_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cotas_Consorcios_consorcioId",
                        column: x => x.consorcioId,
                        principalTable: "Consorcios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consorcios_criador_id",
                table: "Consorcios",
                column: "criador_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cotas_consorcioId",
                table: "Cotas",
                column: "consorcioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotas");

            migrationBuilder.DropTable(
                name: "Consorcios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
