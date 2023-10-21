using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class NullableBusinessAddressInClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_business_address_business_address_id",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "business_address_id",
                table: "clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_business_address_business_address_id",
                table: "clients",
                column: "business_address_id",
                principalTable: "business_address",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_business_address_business_address_id",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "business_address_id",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_business_address_business_address_id",
                table: "clients",
                column: "business_address_id",
                principalTable: "business_address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
