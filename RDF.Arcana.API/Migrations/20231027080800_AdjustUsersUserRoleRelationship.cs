using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustUsersUserRoleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_user_role_id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "user_role_id",
                table: "users",
                newName: "user_roles_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_roles_id",
                table: "users",
                column: "user_roles_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_roles_user_roles_id",
                table: "users",
                column: "user_roles_id",
                principalTable: "user_roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_user_roles_user_roles_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_user_roles_id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "user_roles_id",
                table: "users",
                newName: "user_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_role_id",
                table: "users",
                column: "user_role_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users",
                column: "user_role_id",
                principalTable: "user_roles",
                principalColumn: "id");
        }
    }
}
