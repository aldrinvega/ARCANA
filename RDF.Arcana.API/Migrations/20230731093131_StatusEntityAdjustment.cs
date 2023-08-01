using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class StatusEntityAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_registration_status_registration_status_id",
                table: "clients");

            migrationBuilder.DropTable(
                name: "registration_status");

            migrationBuilder.DropIndex(
                name: "ix_clients_registration_status_id",
                table: "clients");

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status", x => x.id);
                    table.ForeignKey(
                        name: "fk_status_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_status_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clients_status_id",
                table: "clients",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_status_added_by",
                table: "status",
                column: "added_by",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_status_modified_by",
                table: "status",
                column: "modified_by",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_status_status_id",
                table: "clients",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_status_status_id",
                table: "clients");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropIndex(
                name: "ix_clients_status_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "clients");

            migrationBuilder.CreateTable(
                name: "registration_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_registration_status", x => x.id);
                    table.ForeignKey(
                        name: "fk_registration_status_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_registration_status_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clients_registration_status_id",
                table: "clients",
                column: "registration_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_registration_status_added_by",
                table: "registration_status",
                column: "added_by",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_registration_status_modified_by",
                table: "registration_status",
                column: "modified_by",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_registration_status_registration_status_id",
                table: "clients",
                column: "registration_status_id",
                principalTable: "registration_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
