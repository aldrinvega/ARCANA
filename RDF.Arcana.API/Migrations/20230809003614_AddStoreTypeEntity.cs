using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_approved_clients_client_client_id",
                table: "approved_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_clients_status_approved_status_id",
                table: "approved_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_clients_users_approved_by_user_id",
                table: "approved_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies");

            migrationBuilder.DropTable(
                name: "rejected_clients");

            migrationBuilder.DropTable(
                name: "requested_clients");

            migrationBuilder.DropPrimaryKey(
                name: "pk_approved_clients",
                table: "approved_clients");

            migrationBuilder.DropIndex(
                name: "ix_approved_clients_approved_by",
                table: "approved_clients");

            migrationBuilder.DropIndex(
                name: "ix_approved_clients_status",
                table: "approved_clients");

            migrationBuilder.RenameTable(
                name: "approved_clients",
                newName: "approved_client");

            migrationBuilder.RenameIndex(
                name: "ix_approved_clients_client_id",
                table: "approved_client",
                newName: "ix_approved_client_client_id");

            migrationBuilder.AddColumn<int>(
                name: "approved_by_user_id",
                table: "approved_client",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "approved_status_id",
                table: "approved_client",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_approved_client",
                table: "approved_client",
                column: "id");

            migrationBuilder.CreateTable(
                name: "store_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    store_type_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_store_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_store_types_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_store_types_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_approved_client_approved_by_user_id",
                table: "approved_client",
                column: "approved_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_approved_client_approved_status_id",
                table: "approved_client",
                column: "approved_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_store_types_added_by",
                table: "store_types",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_store_types_modified_by",
                table: "store_types",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_approved_client_client_client_id",
                table: "approved_client",
                column: "client_id",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_client_status_approved_status_id",
                table: "approved_client",
                column: "approved_status_id",
                principalTable: "status",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_approved_client_users_approved_by_user_id",
                table: "approved_client",
                column: "approved_by_user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_approved_client_approved_client_id",
                table: "freebies",
                column: "approved_client_id",
                principalTable: "approved_client",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_approved_client_client_client_id",
                table: "approved_client");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_client_status_approved_status_id",
                table: "approved_client");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_client_users_approved_by_user_id",
                table: "approved_client");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_approved_client_approved_client_id",
                table: "freebies");

            migrationBuilder.DropTable(
                name: "store_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_approved_client",
                table: "approved_client");

            migrationBuilder.DropIndex(
                name: "ix_approved_client_approved_by_user_id",
                table: "approved_client");

            migrationBuilder.DropIndex(
                name: "ix_approved_client_approved_status_id",
                table: "approved_client");

            migrationBuilder.DropColumn(
                name: "approved_by_user_id",
                table: "approved_client");

            migrationBuilder.DropColumn(
                name: "approved_status_id",
                table: "approved_client");

            migrationBuilder.RenameTable(
                name: "approved_client",
                newName: "approved_clients");

            migrationBuilder.RenameIndex(
                name: "ix_approved_client_client_id",
                table: "approved_clients",
                newName: "ix_approved_clients_client_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_approved_clients",
                table: "approved_clients",
                column: "id");

            migrationBuilder.CreateTable(
                name: "rejected_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    rejected_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    date_rejected = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejected_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_rejected_clients_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rejected_clients_status_rejected_status_id",
                        column: x => x.status,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rejected_clients_users_rejected_by_user_id",
                        column: x => x.rejected_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "requested_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    requested_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    date_request = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requested_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_requested_clients_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_requested_clients_status_request_status_id",
                        column: x => x.status,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_requested_clients_users_requested_by_user_id",
                        column: x => x.requested_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_approved_clients_approved_by",
                table: "approved_clients",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approved_clients_status",
                table: "approved_clients",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_client_id",
                table: "rejected_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_rejected_by",
                table: "rejected_clients",
                column: "rejected_by",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_status",
                table: "rejected_clients",
                column: "status",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_client_id",
                table: "requested_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_requested_by",
                table: "requested_clients",
                column: "requested_by",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_status",
                table: "requested_clients",
                column: "status",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_clients_client_client_id",
                table: "approved_clients",
                column: "client_id",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_clients_status_approved_status_id",
                table: "approved_clients",
                column: "status",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_clients_users_approved_by_user_id",
                table: "approved_clients",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_approved_clients_approved_client_id",
                table: "freebies",
                column: "approved_client_id",
                principalTable: "approved_clients",
                principalColumn: "id");
        }
    }
}
