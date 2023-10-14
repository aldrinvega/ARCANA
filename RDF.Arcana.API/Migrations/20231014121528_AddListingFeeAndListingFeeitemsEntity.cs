using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddListingFeeAndListingFeeitemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "listing_fees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    crated_at = table.Column<DateOnly>(type: "date", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_approved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    requested_by = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listing_fees", x => x.id);
                    table.ForeignKey(
                        name: "fk_listing_fees_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_listing_fees_users_approved_by_user_id",
                        column: x => x.approved_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_listing_fees_users_requested_by_user_id",
                        column: x => x.requested_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "listing_fee_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    listing_fee_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    sku = table.Column<int>(type: "int", nullable: false),
                    unit_cost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listing_fee_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_listing_fee_items_listing_fees_listing_fee_id",
                        column: x => x.listing_fee_id,
                        principalTable: "listing_fees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fee_items_listing_fee_id",
                table: "listing_fee_items",
                column: "listing_fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_approved_by",
                table: "listing_fees",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_client_id",
                table: "listing_fees",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_requested_by",
                table: "listing_fees",
                column: "requested_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "listing_fee_items");

            migrationBuilder.DropTable(
                name: "listing_fees");
        }
    }
}