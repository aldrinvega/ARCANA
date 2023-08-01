using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustClientEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_users_approved_by_user_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_clients_users_modified_by_user_id",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "approved_by",
                table: "clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_approved_by_user_id",
                table: "clients",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_modified_by_user_id",
                table: "clients",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_users_approved_by_user_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fk_clients_users_modified_by_user_id",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "approved_by",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_approved_by_user_id",
                table: "clients",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_modified_by_user_id",
                table: "clients",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
