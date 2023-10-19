using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddItemRelationshiptoListingFeeItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "approval_id",
                table: "listing_fees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_approval_id",
                table: "listing_fees",
                column: "approval_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_listing_fee_items_item_id",
                table: "listing_fee_items",
                column: "item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fee_items_items_item_id",
                table: "listing_fee_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_approvals_approval_id",
                table: "listing_fees",
                column: "approval_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_listing_fee_items_items_item_id",
                table: "listing_fee_items");

            migrationBuilder.DropForeignKey(
                name: "fk_listing_fees_approvals_approval_id",
                table: "listing_fees");

            migrationBuilder.DropIndex(
                name: "ix_listing_fees_approval_id",
                table: "listing_fees");

            migrationBuilder.DropIndex(
                name: "ix_listing_fee_items_item_id",
                table: "listing_fee_items");

            migrationBuilder.DropColumn(
                name: "approval_id",
                table: "listing_fees");
        }
    }
}
