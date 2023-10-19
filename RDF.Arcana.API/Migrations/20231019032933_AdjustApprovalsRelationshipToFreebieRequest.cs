using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustApprovalsRelationshipToFreebieRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approvals_approval_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_approval_id",
                table: "freebie_requests");

            migrationBuilder.RenameColumn(
                name: "approval_id",
                table: "freebie_requests",
                newName: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approvals_id",
                table: "freebie_requests",
                column: "approvals_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approvals_approvals_id",
                table: "freebie_requests",
                column: "approvals_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approvals_approvals_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_approvals_id",
                table: "freebie_requests");

            migrationBuilder.RenameColumn(
                name: "approvals_id",
                table: "freebie_requests",
                newName: "approval_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approval_id",
                table: "freebie_requests",
                column: "approval_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approvals_approval_id",
                table: "freebie_requests",
                column: "approval_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
