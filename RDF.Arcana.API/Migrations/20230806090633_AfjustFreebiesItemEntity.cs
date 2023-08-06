using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AfjustFreebiesItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_items_freebie_requests_freebie_request_id",
                table: "freebie_items");

            migrationBuilder.DropIndex(
                name: "ix_freebie_items_freebie_request_id",
                table: "freebie_items");

            migrationBuilder.DropColumn(
                name: "freebie_request_id",
                table: "freebie_items");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_items_request_id",
                table: "freebie_items",
                column: "request_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_items_freebie_requests_request_id",
                table: "freebie_items",
                column: "request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_items_freebie_requests_request_id",
                table: "freebie_items");

            migrationBuilder.DropIndex(
                name: "ix_freebie_items_request_id",
                table: "freebie_items");

            migrationBuilder.AddColumn<int>(
                name: "freebie_request_id",
                table: "freebie_items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_freebie_items_freebie_request_id",
                table: "freebie_items",
                column: "freebie_request_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_items_freebie_requests_freebie_request_id",
                table: "freebie_items",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id");
        }
    }
}
