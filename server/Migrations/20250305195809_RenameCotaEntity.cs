using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCotas.Migrations
{
    /// <inheritdoc />
    public partial class RenameCotaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Cotas",
                newName: "valor");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Cotas",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "NumeroCota",
                table: "Cotas",
                newName: "numeroCota");

            migrationBuilder.RenameColumn(
                name: "ConsorcioId",
                table: "Cotas",
                newName: "consorcioId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cotas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataUpdate",
                table: "Cotas",
                newName: "data_update");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Cotas",
                newName: "data_create");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "valor",
                table: "Cotas",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Cotas",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "numeroCota",
                table: "Cotas",
                newName: "NumeroCota");

            migrationBuilder.RenameColumn(
                name: "consorcioId",
                table: "Cotas",
                newName: "ConsorcioId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Cotas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_update",
                table: "Cotas",
                newName: "DataUpdate");

            migrationBuilder.RenameColumn(
                name: "data_create",
                table: "Cotas",
                newName: "DataCriacao");
        }
    }
}
