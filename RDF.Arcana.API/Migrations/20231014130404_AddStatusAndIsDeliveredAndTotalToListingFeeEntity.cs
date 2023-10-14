using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndIsDeliveredAndTotalToListingFeeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_approved",
                table: "listing_fees",
                newName: "is_delivered");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "listing_fees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "total",
                table: "listing_fees",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "listing_fees");

            migrationBuilder.DropColumn(
                name: "total",
                table: "listing_fees");

            migrationBuilder.RenameColumn(
                name: "is_delivered",
                table: "listing_fees",
                newName: "is_approved");
        }
    }
}
