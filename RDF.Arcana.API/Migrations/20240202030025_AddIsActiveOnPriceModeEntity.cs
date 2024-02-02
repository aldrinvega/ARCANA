using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveOnPriceModeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "price_mode",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2338), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2345), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2346) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2349), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2353), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2374) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2377), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2378) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2441));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2480));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 433, DateTimeKind.Local).AddTicks(5213), "$2a$11$z5E03fJGzvOwaH7qvon5teqtHvfDI3mgF5Nd2wW/WHrIsCOYE29cS", new DateTime(2024, 2, 2, 11, 0, 24, 433, DateTimeKind.Local).AddTicks(5240) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "price_mode");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7845), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7849) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7860), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7863) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7870), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7881) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7896), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7982) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7995), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8003) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8253));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8336));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 25, 851, DateTimeKind.Local).AddTicks(8243), "$2a$11$e9tpvxPFzLoQQA4XmHkPxeNJH9f.YU8cRsVcBkoxxAVMDFOFxomgi", new DateTime(2024, 2, 2, 10, 29, 25, 851, DateTimeKind.Local).AddTicks(8256) });
        }
    }
}
