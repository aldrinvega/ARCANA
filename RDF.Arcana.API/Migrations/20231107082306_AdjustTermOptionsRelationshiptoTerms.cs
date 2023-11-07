using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTermOptionsRelationshiptoTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_term_options_terms_id",
                table: "term_options");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_terms_id",
                table: "term_options",
                column: "terms_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_term_options_terms_id",
                table: "term_options");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_terms_id",
                table: "term_options",
                column: "terms_id");
        }
    }
}
