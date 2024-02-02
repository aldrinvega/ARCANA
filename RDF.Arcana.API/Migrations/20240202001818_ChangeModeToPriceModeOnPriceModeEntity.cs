using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModeToPriceModeOnPriceModeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_price_mode_users_modified_by_user_id",
                table: "price_mode");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "price_mode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9812), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9813) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9816), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9817) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9818), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9819) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9820), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9832) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9833), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9834) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9917));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9921));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9875));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9719));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 451, DateTimeKind.Local).AddTicks(8765), "$2a$11$6bEr3Y7RoAclcLYZd2jPFOYa1cMiRnmahaT31ERWu4VD0trpDpbHq", new DateTime(2024, 2, 2, 8, 18, 17, 451, DateTimeKind.Local).AddTicks(8787) });

            migrationBuilder.AddForeignKey(
                name: "fk_price_mode_users_modified_by_user_id",
                table: "price_mode",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_price_mode_users_modified_by_user_id",
                table: "price_mode");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "price_mode",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3094), new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3096) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3100), new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3101) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3103), new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3122), new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3123) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3126), new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3126) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3341));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3188));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3251));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(3253));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 1, 11, 26, 46, 788, DateTimeKind.Local).AddTicks(2974));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 26, 46, 181, DateTimeKind.Local).AddTicks(6875), "$2a$11$O7VpRfnO0QJPgScU.7.Er.8sGOqRFUfGBRD0/5xtLrfzKkDKsff3u", new DateTime(2024, 2, 1, 11, 26, 46, 181, DateTimeKind.Local).AddTicks(6896) });

            migrationBuilder.AddForeignKey(
                name: "fk_price_mode_users_modified_by_user_id",
                table: "price_mode",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
