using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyToFreebiesItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approved_clients_approved_client_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approved_clients_client_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_status_status_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_users_added_by",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_freebie_requests_freebie_request_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_rejected_freebies_rejected_freebies_id",
                table: "freebies");

            migrationBuilder.DropTable(
                name: "freebie_request");

            migrationBuilder.DropTable(
                name: "rejected_freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_client_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_freebie_request_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_items_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_added_by",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_client_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_status_id",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "freebie_request_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "items_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "freebie_requests");

            migrationBuilder.RenameColumn(
                name: "rejected_freebies_id",
                table: "freebies",
                newName: "approved_client_id");

            migrationBuilder.RenameIndex(
                name: "ix_freebies_rejected_freebies_id",
                table: "freebies",
                newName: "ix_freebies_approved_client_id");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "freebie_requests",
                newName: "approval_id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "freebie_requests",
                newName: "is_delivered");

            migrationBuilder.RenameColumn(
                name: "approved_client_id",
                table: "freebie_requests",
                newName: "clients_id");

            migrationBuilder.RenameIndex(
                name: "ix_freebie_requests_approved_client_id",
                table: "freebie_requests",
                newName: "ix_freebie_requests_clients_id");

            migrationBuilder.AddColumn<string>(
                name: "photo_proof_path",
                table: "freebie_requests",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "freebie_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    request_id = table.Column<int>(type: "int", nullable: false),
                    freebie_request_id = table.Column<int>(type: "int", nullable: true),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_items_freebie_requests_freebie_request_id",
                        column: x => x.freebie_request_id,
                        principalTable: "freebie_requests",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_freebie_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approval_id",
                table: "freebie_requests",
                column: "approval_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_freebie_items_freebie_request_id",
                table: "freebie_items",
                column: "freebie_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_items_item_id",
                table: "freebie_items",
                column: "item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approvals_approval_id",
                table: "freebie_requests",
                column: "approval_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_clients_clients_id",
                table: "freebie_requests",
                column: "clients_id",
                principalTable: "clients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies",
                column: "approved_client_id",
                principalTable: "approved_clients",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approvals_approval_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_clients_clients_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies");

            migrationBuilder.DropTable(
                name: "freebie_items");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_approval_id",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "photo_proof_path",
                table: "freebie_requests");

            migrationBuilder.RenameColumn(
                name: "approved_client_id",
                table: "freebies",
                newName: "rejected_freebies_id");

            migrationBuilder.RenameIndex(
                name: "ix_freebies_approved_client_id",
                table: "freebies",
                newName: "ix_freebies_rejected_freebies_id");

            migrationBuilder.RenameColumn(
                name: "is_delivered",
                table: "freebie_requests",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "clients_id",
                table: "freebie_requests",
                newName: "approved_client_id");

            migrationBuilder.RenameColumn(
                name: "approval_id",
                table: "freebie_requests",
                newName: "status_id");

            migrationBuilder.RenameIndex(
                name: "ix_freebie_requests_clients_id",
                table: "freebie_requests",
                newName: "ix_freebie_requests_approved_client_id");

            migrationBuilder.AddColumn<int>(
                name: "freebie_request_id",
                table: "freebies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "items_id",
                table: "freebies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "added_by",
                table: "freebie_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "freebie_requests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "freebie_requests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "freebie_request",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    approvals_id = table.Column<int>(type: "int", nullable: true),
                    approval_id = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    clients_id = table.Column<int>(type: "int", nullable: true),
                    is_approved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_request", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_request_approvals_approvals_id",
                        column: x => x.approvals_id,
                        principalTable: "approvals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_freebie_request_clients_clients_id",
                        column: x => x.clients_id,
                        principalTable: "clients",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rejected_freebies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    freebies_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    transaction_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejected_freebies", x => x.id);
                    table.ForeignKey(
                        name: "fk_rejected_freebies_freebies_freebies_id",
                        column: x => x.freebies_id,
                        principalTable: "freebies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rejected_freebies_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rejected_freebies_users_added_by",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_client_id",
                table: "freebies",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_freebie_request_id",
                table: "freebies",
                column: "freebie_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_items_id",
                table: "freebies",
                column: "items_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_added_by",
                table: "freebie_requests",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_client_id",
                table: "freebie_requests",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_status_id",
                table: "freebie_requests",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_request_approvals_id",
                table: "freebie_request",
                column: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_request_clients_id",
                table: "freebie_request",
                column: "clients_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_added_by",
                table: "rejected_freebies",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_freebies_id",
                table: "rejected_freebies",
                column: "freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_status_id",
                table: "rejected_freebies",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approved_clients_approved_client_id",
                table: "freebie_requests",
                column: "approved_client_id",
                principalTable: "approved_clients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approved_clients_client_id",
                table: "freebie_requests",
                column: "client_id",
                principalTable: "approved_clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_status_status_id",
                table: "freebie_requests",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_users_added_by",
                table: "freebie_requests",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies",
                column: "client_id",
                principalTable: "approved_clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_freebie_requests_freebie_request_id",
                table: "freebies",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id");

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
        }
    }
}
