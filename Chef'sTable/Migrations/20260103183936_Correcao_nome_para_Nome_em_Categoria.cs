using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chef_sTable.Migrations
{
    /// <inheritdoc />
    public partial class Correcao_nome_para_Nome_em_Categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Categoria",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Categoria",
                newName: "nome");
        }
    }
}
