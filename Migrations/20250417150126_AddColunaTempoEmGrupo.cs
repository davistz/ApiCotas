using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCotas.Migrations
{
    /// <inheritdoc />
    public partial class AddColunaTempoEmGrupo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "tempo",
                table: "Consorcios",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tempo",
                table: "Consorcios");
        }
    }
}
