using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustFreebieEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "requested_by",
                table: "freebie_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "approved_by",
                table: "approvals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requested_by",
                table: "approvals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_requested_by",
                table: "freebie_requests",
                column: "requested_by");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_approved_by",
                table: "approvals",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_requested_by",
                table: "approvals",
                column: "requested_by");

            migrationBuilder.AddForeignKey(
                name: "fk_approvals_users_approve_by_user_id",
                table: "approvals",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approvals_users_requested_by_user_id",
                table: "approvals",
                column: "requested_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_users_requested_by_user_id",
                table: "freebie_requests",
                column: "requested_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_approvals_users_approve_by_user_id",
                table: "approvals");

            migrationBuilder.DropForeignKey(
                name: "fk_approvals_users_requested_by_user_id",
                table: "approvals");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_users_requested_by_user_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_requested_by",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_approvals_approved_by",
                table: "approvals");

            migrationBuilder.DropIndex(
                name: "ix_approvals_requested_by",
                table: "approvals");

            migrationBuilder.DropColumn(
                name: "requested_by",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "approved_by",
                table: "approvals");

            migrationBuilder.DropColumn(
                name: "requested_by",
                table: "approvals");
        }
    }
}
