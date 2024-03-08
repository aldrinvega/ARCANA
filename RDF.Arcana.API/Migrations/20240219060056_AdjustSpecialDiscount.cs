using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustSpecialDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "discount",
                table: "special_discounts",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7101), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7101) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7104), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7106), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7106) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7107), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7127), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7128) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7181));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7011));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 83, DateTimeKind.Local).AddTicks(281), "$2a$11$WHyNAlYWdi4rYE4xnsmTBeLu1aNWPxBsPN.NLtVuoijZEL6p8No3W", new DateTime(2024, 2, 19, 14, 0, 56, 83, DateTimeKind.Local).AddTicks(295) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "discount",
                table: "special_discounts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(403), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(403) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(406), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(407) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(408), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(408) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(409), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(423) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(425), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(426) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(510));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(514));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(460));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(478));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 569, DateTimeKind.Local).AddTicks(3981), "$2a$11$n8CUbyQBmW88foI3Iwzede52ptRAmepTnuCvb2eT32evkiC8o1G6e", new DateTime(2024, 2, 16, 11, 19, 31, 569, DateTimeKind.Local).AddTicks(3998) });
        }
    }
}
