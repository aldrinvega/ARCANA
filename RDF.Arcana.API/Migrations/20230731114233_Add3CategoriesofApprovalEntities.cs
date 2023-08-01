using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class Add3CategoriesofApprovalEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_status_status_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_clients_users_approved_by_user_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_approved_by",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_status_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "approved_by",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "registration_status_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "clients");

            migrationBuilder.CreateTable(
                name: "approved_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_approved = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approved_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_approved_clients_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approved_clients_status_approved_status_id",
                        column: x => x.status,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approved_clients_users_approved_by_user_id",
                        column: x => x.approved_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rejected_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_rejected = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    rejected_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejected_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_rejected_clients_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
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
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_request = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    requested_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requested_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_requested_clients_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
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
                name: "ix_approved_clients_client_id",
                table: "approved_clients",
                column: "client_id");

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
                column: "rejected_by");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_status",
                table: "rejected_clients",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_client_id",
                table: "requested_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_requested_by",
                table: "requested_clients",
                column: "requested_by");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_status",
                table: "requested_clients",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approved_clients");

            migrationBuilder.DropTable(
                name: "rejected_clients");

            migrationBuilder.DropTable(
                name: "requested_clients");

            migrationBuilder.AddColumn<int>(
                name: "approved_by",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "registration_status_id",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_approved_by",
                table: "clients",
                column: "approved_by",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_status_id",
                table: "clients",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_status_status_id",
                table: "clients",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_approved_by_user_id",
                table: "clients",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
