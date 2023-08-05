using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustFreebies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_approved_freebies_freebie_requests_freebie_request_id",
                table: "approved_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_freebie_requests_freebie_request_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_freebie_requests_freebie_request_id",
                table: "rejected_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_users_rejected_by",
                table: "rejected_freebies");

            migrationBuilder.RenameColumn(
                name: "rejection_reason",
                table: "rejected_freebies",
                newName: "transaction_number");

            migrationBuilder.RenameColumn(
                name: "rejected_by",
                table: "rejected_freebies",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "rejected_at",
                table: "rejected_freebies",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "freebie_request_id",
                table: "rejected_freebies",
                newName: "freebies_id");

            migrationBuilder.RenameIndex(
                name: "ix_rejected_freebies_rejected_by",
                table: "rejected_freebies",
                newName: "ix_rejected_freebies_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_rejected_freebies_freebie_request_id",
                table: "rejected_freebies",
                newName: "ix_rejected_freebies_freebies_id");

            migrationBuilder.RenameColumn(
                name: "freebie_request_id",
                table: "approved_freebies",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "approved_at",
                table: "approved_freebies",
                newName: "updated_at");

            migrationBuilder.RenameIndex(
                name: "ix_approved_freebies_freebie_request_id",
                table: "approved_freebies",
                newName: "ix_approved_freebies_status_id");

            migrationBuilder.AddColumn<int>(
                name: "added_by",
                table: "rejected_freebies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "rejected_freebies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "rejected_freebies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "freebie_request_id",
                table: "freebies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "approved_freebies_id",
                table: "freebies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "freebies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "items_id",
                table: "freebies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rejected_freebies_id",
                table: "freebies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "approved_client_id",
                table: "freebie_requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "approved_freebies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "freebies_id",
                table: "approved_freebies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "approved_freebies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "transaction_number",
                table: "approved_freebies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_added_by",
                table: "rejected_freebies",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_approved_freebies_id",
                table: "freebies",
                column: "approved_freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_client_id",
                table: "freebies",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_items_id",
                table: "freebies",
                column: "items_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_rejected_freebies_id",
                table: "freebies",
                column: "rejected_freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approved_client_id",
                table: "freebie_requests",
                column: "approved_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_approved_freebies_freebies_id",
                table: "approved_freebies",
                column: "freebies_id");

            migrationBuilder.AddForeignKey(
                name: "fk_approved_freebies_freebies_freebies_id",
                table: "approved_freebies",
                column: "freebies_id",
                principalTable: "freebies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_freebies_status_status_id",
                table: "approved_freebies",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approved_clients_approved_client_id",
                table: "freebie_requests",
                column: "approved_client_id",
                principalTable: "approved_clients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies",
                column: "client_id",
                principalTable: "approved_clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_approved_freebies_approved_freebies_id",
                table: "freebies",
                column: "approved_freebies_id",
                principalTable: "approved_freebies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_freebie_requests_freebie_request_id",
                table: "freebies",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_items_item_id",
                table: "freebies",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies",
                column: "items_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_rejected_freebies_rejected_freebies_id",
                table: "freebies",
                column: "rejected_freebies_id",
                principalTable: "rejected_freebies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_freebies_freebies_id",
                table: "rejected_freebies",
                column: "freebies_id",
                principalTable: "freebies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_status_status_id",
                table: "rejected_freebies",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_users_added_by",
                table: "rejected_freebies",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_approved_freebies_freebies_freebies_id",
                table: "approved_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_freebies_status_status_id",
                table: "approved_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approved_clients_approved_client_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_approved_freebies_approved_freebies_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_freebie_requests_freebie_request_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_item_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_rejected_freebies_rejected_freebies_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_freebies_freebies_id",
                table: "rejected_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_status_status_id",
                table: "rejected_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_users_added_by",
                table: "rejected_freebies");

            migrationBuilder.DropIndex(
                name: "ix_rejected_freebies_added_by",
                table: "rejected_freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_approved_freebies_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_client_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_items_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_rejected_freebies_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_approved_client_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_approved_freebies_freebies_id",
                table: "approved_freebies");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "rejected_freebies");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "rejected_freebies");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "rejected_freebies");

            migrationBuilder.DropColumn(
                name: "approved_freebies_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "items_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "rejected_freebies_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "approved_client_id",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "approved_freebies");

            migrationBuilder.DropColumn(
                name: "freebies_id",
                table: "approved_freebies");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "approved_freebies");

            migrationBuilder.DropColumn(
                name: "transaction_number",
                table: "approved_freebies");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "rejected_freebies",
                newName: "rejected_at");

            migrationBuilder.RenameColumn(
                name: "transaction_number",
                table: "rejected_freebies",
                newName: "rejection_reason");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "rejected_freebies",
                newName: "rejected_by");

            migrationBuilder.RenameColumn(
                name: "freebies_id",
                table: "rejected_freebies",
                newName: "freebie_request_id");

            migrationBuilder.RenameIndex(
                name: "ix_rejected_freebies_status_id",
                table: "rejected_freebies",
                newName: "ix_rejected_freebies_rejected_by");

            migrationBuilder.RenameIndex(
                name: "ix_rejected_freebies_freebies_id",
                table: "rejected_freebies",
                newName: "ix_rejected_freebies_freebie_request_id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "approved_freebies",
                newName: "approved_at");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "approved_freebies",
                newName: "freebie_request_id");

            migrationBuilder.RenameIndex(
                name: "ix_approved_freebies_status_id",
                table: "approved_freebies",
                newName: "ix_approved_freebies_freebie_request_id");

            migrationBuilder.AlterColumn<int>(
                name: "freebie_request_id",
                table: "freebies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_freebies_freebie_requests_freebie_request_id",
                table: "approved_freebies",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_freebie_requests_freebie_request_id",
                table: "freebies",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_freebie_requests_freebie_request_id",
                table: "rejected_freebies",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_users_rejected_by",
                table: "rejected_freebies",
                column: "rejected_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
