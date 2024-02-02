using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPriceModeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mode",
                table: "price_mode",
                newName: "price_mode_code");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_mode_code",
                table: "price_mode",
                newName: "mode");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7236), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7237) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7240), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7240) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7242), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7257) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7259), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7259) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7261), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7261) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7317));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7162));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 49, 612, DateTimeKind.Local).AddTicks(6555), "$2a$11$fHxvm6AYyji3LDXuNNM9xOEuTFDbJD.DnBvjGJWHZ.ftV6l3skRDi", new DateTime(2024, 1, 29, 13, 40, 49, 612, DateTimeKind.Local).AddTicks(6572) });
        }
    }
}
