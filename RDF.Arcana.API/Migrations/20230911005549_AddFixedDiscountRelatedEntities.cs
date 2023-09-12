using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFixedDiscountRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_fixed_discounts_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "discount_type",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "fixed_discounts_id",
                table: "clients");

            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "fixed_discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fixed_discount_id",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "variable_discount",
                table: "clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "ix_fixed_discounts_client_id",
                table: "fixed_discounts",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_fixed_discount_id",
                table: "clients",
                column: "fixed_discount_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients",
                column: "fixed_discount_id",
                principalTable: "fixed_discounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_fixed_discounts_clients_clients_id",
                table: "fixed_discounts",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_fixed_discounts_clients_clients_id",
                table: "fixed_discounts");

            migrationBuilder.DropIndex(
                name: "ix_fixed_discounts_client_id",
                table: "fixed_discounts");

            migrationBuilder.DropIndex(
                name: "ix_clients_fixed_discount_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "fixed_discounts");

            migrationBuilder.DropColumn(
                name: "fixed_discount_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "variable_discount",
                table: "clients");

            migrationBuilder.AddColumn<short>(
                name: "discount_type",
                table: "clients",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "fixed_discounts_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_fixed_discounts_id",
                table: "clients",
                column: "fixed_discounts_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients",
                column: "fixed_discounts_id",
                principalTable: "fixed_discounts",
                principalColumn: "id");
        }
    }
}
