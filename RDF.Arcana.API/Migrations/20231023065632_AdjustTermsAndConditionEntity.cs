using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTermsAndConditionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_term_options_terms_terms_id",
                table: "term_options");

            migrationBuilder.DropColumn(
                name: "term_id",
                table: "term_options");

            migrationBuilder.AlterColumn<int>(
                name: "terms_id",
                table: "term_options",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "listing_fees",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_percentage",
                table: "fixed_discounts",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_terms_terms_id",
                table: "term_options",
                column: "terms_id",
                principalTable: "terms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_term_options_terms_terms_id",
                table: "term_options");

            migrationBuilder.AlterColumn<int>(
                name: "terms_id",
                table: "term_options",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "term_id",
                table: "term_options",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "listing_fees",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_percentage",
                table: "fixed_discounts",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_terms_terms_id",
                table: "term_options",
                column: "terms_id",
                principalTable: "terms",
                principalColumn: "id");
        }
    }
}
