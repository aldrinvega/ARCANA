using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustFreezerAssetTagColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assete_tag",
                table: "freezers",
                newName: "asset_tag");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1125), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1125) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1129), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1129) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1130), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1131) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1132), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1373) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1381), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1381) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1467));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1041));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 784, DateTimeKind.Local).AddTicks(1668), "$2a$11$h98qeG0Qiwx5hPV1ji/CqOgl9WrNKafX8kMr3kQ9hV0s3OBtpvPpa", new DateTime(2024, 3, 13, 9, 28, 22, 784, DateTimeKind.Local).AddTicks(1683) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "asset_tag",
                table: "freezers",
                newName: "assete_tag");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2526), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2527) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2530), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2531) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2533), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2534) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2535), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2548) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2549), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2659));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2664));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2599));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2622));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2624));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 674, DateTimeKind.Local).AddTicks(6254), "$2a$11$D9WL0CNwHBMrZdLEdvzaderqb8BoqKbr4kiTCa51R5UJXe0wjyX66", new DateTime(2024, 3, 12, 8, 45, 19, 674, DateTimeKind.Local).AddTicks(6269) });
        }
    }
}
