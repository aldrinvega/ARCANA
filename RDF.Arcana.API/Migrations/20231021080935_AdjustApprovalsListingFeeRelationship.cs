using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustApprovalsListingFeeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_listing_fees_approvals_approval_id",
                table: "listing_fees");

            migrationBuilder.DropIndex(
                name: "ix_listing_fees_approval_id",
                table: "listing_fees");

            migrationBuilder.RenameColumn(
                name: "approval_id",
                table: "listing_fees",
                newName: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_approvals_id",
                table: "listing_fees",
                column: "approvals_id");

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_approvals_approvals_id",
                table: "listing_fees",
                column: "approvals_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_listing_fees_approvals_approvals_id",
                table: "listing_fees");

            migrationBuilder.DropIndex(
                name: "ix_listing_fees_approvals_id",
                table: "listing_fees");

            migrationBuilder.RenameColumn(
                name: "approvals_id",
                table: "listing_fees",
                newName: "approval_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_approval_id",
                table: "listing_fees",
                column: "approval_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_approvals_approval_id",
                table: "listing_fees",
                column: "approval_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
