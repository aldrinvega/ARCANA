using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToFreebieEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "freebie_requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9694), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9695) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9700), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9701) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9703), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9703) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9705), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9722) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9724), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9725) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9846));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9548));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 379, DateTimeKind.Local).AddTicks(8281), "$2a$11$UN.Dx8IjoR6hmNVK68Mv5OyoUnS/RklN7hBAUDClttV6yK/lkfoHi", new DateTime(2024, 1, 11, 14, 33, 26, 379, DateTimeKind.Local).AddTicks(8359) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "freebie_requests");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2447), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2448) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2451), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2452) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2453), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2454) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2456), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2523) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2524), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2525) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2662));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2728));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 53, 921, DateTimeKind.Local).AddTicks(2067), "$2a$11$pqm44LFK5YEYUEHc74kgSeP3o7VmiK68Hu/5gwWMZwGwTsB4dnpRy", new DateTime(2024, 1, 11, 11, 31, 53, 921, DateTimeKind.Local).AddTicks(2080) });
        }
    }
}
