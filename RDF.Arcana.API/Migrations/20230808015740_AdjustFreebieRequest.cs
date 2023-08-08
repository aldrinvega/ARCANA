using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustFreebieRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_clients_clients_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_clients_id",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "clients_id",
                table: "freebie_requests");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_client_id",
                table: "freebie_requests",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_clients_client_id",
                table: "freebie_requests",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_clients_client_id",
                table: "freebie_requests");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_client_id",
                table: "freebie_requests");

            migrationBuilder.AddColumn<int>(
                name: "clients_id",
                table: "freebie_requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_clients_id",
                table: "freebie_requests",
                column: "clients_id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_clients_clients_id",
                table: "freebie_requests",
                column: "clients_id",
                principalTable: "clients",
                principalColumn: "id");
        }
    }
}
