using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustStoreTypeInClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "store_type",
                table: "clients");

            migrationBuilder.AddColumn<int>(
                name: "store_type_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_store_type_id",
                table: "clients",
                column: "store_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_store_types_store_type_id",
                table: "clients",
                column: "store_type_id",
                principalTable: "store_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_store_types_store_type_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_store_type_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "store_type_id",
                table: "clients");

            migrationBuilder.AddColumn<string>(
                name: "store_type",
                table: "clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
