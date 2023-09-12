using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDiscountOnClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_term_options_term_days_term_days_id",
                table: "term_options");

            migrationBuilder.AlterColumn<int>(
                name: "term_days_id",
                table: "term_options",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "variable_discount",
                table: "clients",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "fixed_discount_id",
                table: "clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients",
                column: "fixed_discount_id",
                principalTable: "fixed_discounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_term_days_term_days_id",
                table: "term_options",
                column: "term_days_id",
                principalTable: "term_days",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_term_options_term_days_term_days_id",
                table: "term_options");

            migrationBuilder.AlterColumn<int>(
                name: "term_days_id",
                table: "term_options",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "variable_discount",
                table: "clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fixed_discount_id",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients",
                column: "fixed_discount_id",
                principalTable: "fixed_discounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_term_days_term_days_id",
                table: "term_options",
                column: "term_days_id",
                principalTable: "term_days",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
