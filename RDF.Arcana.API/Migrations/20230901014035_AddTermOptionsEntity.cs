using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTermOptionsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "term",
                table: "terms",
                newName: "term_type");

            migrationBuilder.CreateTable(
                name: "term_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    term_id = table.Column<int>(type: "int", nullable: false),
                    credit_limit = table.Column<int>(type: "int", nullable: false),
                    term_days_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    terms_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_term_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_term_options_term_days_term_days_id",
                        column: x => x.term_days_id,
                        principalTable: "term_days",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_term_options_terms_terms_id",
                        column: x => x.terms_id,
                        principalTable: "terms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_term_options_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_added_by",
                table: "term_options",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_term_days_id",
                table: "term_options",
                column: "term_days_id");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_terms_id",
                table: "term_options",
                column: "terms_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "term_options");

            migrationBuilder.RenameColumn(
                name: "term_type",
                table: "terms",
                newName: "term");
        }
    }
}
