using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBoookingCoverageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "booking_coverage",
                table: "client");

            migrationBuilder.AddColumn<int>(
                name: "booking_coverage_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "booking_coverages_id",
                table: "client",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "booking_coverages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_coverage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_coverages", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_coverages_users_added_by",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clients_booking_coverage_id",
                table: "clients",
                column: "booking_coverage_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_booking_coverages_id",
                table: "client",
                column: "booking_coverages_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_coverages_added_by",
                table: "booking_coverages",
                column: "added_by");

            migrationBuilder.AddForeignKey(
                name: "fk_client_booking_coverages_booking_coverages_id",
                table: "client",
                column: "booking_coverages_id",
                principalTable: "booking_coverages",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_booking_coverages_booking_coverages_id",
                table: "clients",
                column: "booking_coverage_id",
                principalTable: "booking_coverages",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_client_booking_coverages_booking_coverages_id",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "fk_clients_booking_coverages_booking_coverages_id",
                table: "clients");

            migrationBuilder.DropTable(
                name: "booking_coverages");

            migrationBuilder.DropIndex(
                name: "ix_clients_booking_coverage_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_client_booking_coverages_id",
                table: "client");

            migrationBuilder.DropColumn(
                name: "booking_coverage_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "booking_coverages_id",
                table: "client");

            migrationBuilder.AddColumn<string>(
                name: "booking_coverage",
                table: "client",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
