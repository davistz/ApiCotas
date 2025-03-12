using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCotas.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consorcios",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    numero_participantes = table.Column<int>(type: "int", nullable: false),
                    data_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consorcios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserConsorcios",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    consorcio_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConsorcios", x => new { x.user_id, x.consorcio_id });
                    table.ForeignKey(
                        name: "FK_UserConsorcios_Consorcios_consorcio_id",
                        column: x => x.consorcio_id,
                        principalTable: "Consorcios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConsorcios_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConsorcios_consorcio_id",
                table: "UserConsorcios",
                column: "consorcio_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConsorcios");

            migrationBuilder.DropTable(
                name: "Consorcios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
