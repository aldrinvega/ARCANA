using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPriceModeItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "i_active",
                table: "price_mode_items",
                newName: "is_active");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8799), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8802) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8806), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8807) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8809), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8812), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8834) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8840), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8843) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(9049));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8916));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8941));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8946));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8670));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 33, DateTimeKind.Local).AddTicks(4376), "$2a$11$KXUJUS2FwgmNUtqe/QIbouyP280herHIrgt3YO0FCQUy2b.p9Wv3u", new DateTime(2024, 2, 3, 21, 24, 42, 33, DateTimeKind.Local).AddTicks(4401) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "price_mode_items",
                newName: "i_active");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3028), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3029) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3033), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3033) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3036), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3037) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3038), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3059) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3061), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3062) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3179));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3111));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3137));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3139));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 85, DateTimeKind.Local).AddTicks(6199), "$2a$11$P0RdDKp0WHd5Xwz2uYa97u70Z1N7IFmKM1CFmATzJv.Tfk6RSIbwy", new DateTime(2024, 2, 3, 18, 1, 14, 85, DateTimeKind.Local).AddTicks(6312) });
        }
    }
}
