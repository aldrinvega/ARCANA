using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintOnUserRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "user_role_name",
                table: "user_roles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "origin",
                table: "advance_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5427), new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5427) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5431), new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5432) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5433), new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5434) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5435), new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5452) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5454), new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5454) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5620));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5495));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5517));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 6, 21, 11, 38, 719, DateTimeKind.Local).AddTicks(5348));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 6, 21, 11, 38, 587, DateTimeKind.Local).AddTicks(9309), "$2a$11$d3s4ODBjAtv8PYL8SBf7kuKYgkZdw12ssMSkknzGUv0s.2MKQQaLO", new DateTime(2024, 4, 6, 21, 11, 38, 587, DateTimeKind.Local).AddTicks(9325) });

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_role_name",
                table: "user_roles",
                column: "user_role_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_roles_user_role_name",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "origin",
                table: "advance_payments");

            migrationBuilder.AlterColumn<string>(
                name: "user_role_name",
                table: "user_roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8226), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8227) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8231), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8232) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8233), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8234) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8235), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8254) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8256), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8256) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8476));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 356, DateTimeKind.Local).AddTicks(1075), "$2a$11$gePchTD6WfWy4YQo27GHQugeqA757kT8.vO/y56MGJkLjxmiKuuuC", new DateTime(2024, 4, 1, 9, 3, 7, 356, DateTimeKind.Local).AddTicks(1090) });
        }
    }
}
