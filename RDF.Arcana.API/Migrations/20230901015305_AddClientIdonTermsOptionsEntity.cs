using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientIdonTermsOptionsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "term_options",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_term_options_client_id",
                table: "term_options",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_clients_clients_id",
                table: "term_options",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_term_options_clients_clients_id",
                table: "term_options");

            migrationBuilder.DropIndex(
                name: "ix_term_options_client_id",
                table: "term_options");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "term_options");
        }
    }
}
