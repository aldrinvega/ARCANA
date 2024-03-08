using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddjustFreebieRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_items_freebie_requests_request_id",
                table: "freebie_items");

            migrationBuilder.RenameColumn(
                name: "request_id",
                table: "freebie_items",
                newName: "freebie_request_id");

            migrationBuilder.RenameIndex(
                name: "ix_freebie_items_request_id",
                table: "freebie_items",
                newName: "ix_freebie_items_freebie_request_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1379), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1382) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1391), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1392) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1395), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1426) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1431), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1432) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1435), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1436) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1582));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1535));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(812));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 4, 377, DateTimeKind.Local).AddTicks(699), "$2a$11$JwJf6En6qUJj/utgWLRgROSgEwIE.1w9E/7Db9LFg6acOtlgrcMvW", new DateTime(2024, 1, 15, 15, 49, 4, 377, DateTimeKind.Local).AddTicks(736) });

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_items_freebie_requests_freebie_request_id",
                table: "freebie_items",
                column: "freebie_request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_items_freebie_requests_freebie_request_id",
                table: "freebie_items");

            migrationBuilder.RenameColumn(
                name: "freebie_request_id",
                table: "freebie_items",
                newName: "request_id");

            migrationBuilder.RenameIndex(
                name: "ix_freebie_items_freebie_request_id",
                table: "freebie_items",
                newName: "ix_freebie_items_request_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9330), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9332) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9336), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9340), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9342), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9360) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9362), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9363) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9491));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9496));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9449));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9183));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 15, 691, DateTimeKind.Local).AddTicks(5898), "$2a$11$R/1TdTyZkzyMFPcAyPA5JutI/N3PIssZdP1rxsIIX2X0rJ9bIeeqa", new DateTime(2024, 1, 15, 11, 2, 15, 691, DateTimeKind.Local).AddTicks(5936) });

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_items_freebie_requests_request_id",
                table: "freebie_items",
                column: "request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
