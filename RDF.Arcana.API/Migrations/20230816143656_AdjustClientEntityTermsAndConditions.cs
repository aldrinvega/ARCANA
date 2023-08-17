using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustClientEntityTermsAndConditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cluster",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "direct_delivery",
                table: "clients",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mode_of_payment",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "terms",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    payment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    term = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_terms", x => x.id);
                    table.ForeignKey(
                        name: "fk_terms_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clients_mode_of_payment",
                table: "clients",
                column: "mode_of_payment");

            migrationBuilder.CreateIndex(
                name: "ix_clients_terms",
                table: "clients",
                column: "terms");

            migrationBuilder.CreateIndex(
                name: "ix_payments_added_by",
                table: "payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_terms_added_by",
                table: "terms",
                column: "added_by");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_payments_mode_of_payments_id",
                table: "clients",
                column: "mode_of_payment",
                principalTable: "payments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_terms_term_id",
                table: "clients",
                column: "terms",
                principalTable: "terms",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_payments_mode_of_payments_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_clients_terms_term_id",
                table: "clients");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "terms");

            migrationBuilder.DropIndex(
                name: "ix_clients_mode_of_payment",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_terms",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "cluster",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "direct_delivery",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "mode_of_payment",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "terms",
                table: "clients");
        }
    }
}
