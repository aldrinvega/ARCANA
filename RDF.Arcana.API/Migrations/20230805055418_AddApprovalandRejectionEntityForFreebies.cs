using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovalandRejectionEntityForFreebies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_item_id",
                table: "freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies");

            migrationBuilder.DropIndex(
                name: "ix_freebies_items_id",
                table: "freebies");

            migrationBuilder.DropColumn(
                name: "items_id",
                table: "freebies");

            migrationBuilder.CreateTable(
                name: "approved_freebies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    freebie_request_id = table.Column<int>(type: "int", nullable: false),
                    photo_proof = table.Column<byte[]>(type: "longblob", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approved_freebies", x => x.id);
                    table.ForeignKey(
                        name: "fk_approved_freebies_freebie_requests_freebie_request_id",
                        column: x => x.freebie_request_id,
                        principalTable: "freebie_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approved_freebies_users_approved_by",
                        column: x => x.approved_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rejected_freebies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    freebie_request_id = table.Column<int>(type: "int", nullable: false),
                    rejection_reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rejected_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    rejected_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejected_freebies", x => x.id);
                    table.ForeignKey(
                        name: "fk_rejected_freebies_freebie_requests_freebie_request_id",
                        column: x => x.freebie_request_id,
                        principalTable: "freebie_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rejected_freebies_users_rejected_by",
                        column: x => x.rejected_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_approved_freebies_approved_by",
                table: "approved_freebies",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approved_freebies_freebie_request_id",
                table: "approved_freebies",
                column: "freebie_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_freebie_request_id",
                table: "rejected_freebies",
                column: "freebie_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_rejected_by",
                table: "rejected_freebies",
                column: "rejected_by");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies");

            migrationBuilder.DropTable(
                name: "approved_freebies");

            migrationBuilder.DropTable(
                name: "rejected_freebies");

            migrationBuilder.AddColumn<int>(
                name: "items_id",
                table: "freebies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_freebies_items_id",
                table: "freebies",
                column: "items_id");

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
        }
    }
}
