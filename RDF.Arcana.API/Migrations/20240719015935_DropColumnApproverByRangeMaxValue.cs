using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnApproverByRangeMaxValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "max_value",
                table: "approver_by_range");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3322), new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3324) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3329), new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3329) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3331), new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3332) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3334), new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3354) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3356), new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3356) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3449));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3454));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3396));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(3416));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 19, 9, 59, 32, 69, DateTimeKind.Local).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 19, 9, 59, 31, 800, DateTimeKind.Local).AddTicks(120), "$2a$11$6/xLDfnL2wq9Q175nEGGfO4LIeKDwA.UmK.G197CMX91ypAvghq6m", new DateTime(2024, 7, 19, 9, 59, 31, 800, DateTimeKind.Local).AddTicks(158) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "max_value",
                table: "approver_by_range",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9899), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9901) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9904), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9904) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9905), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9918), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9918) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9919), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9920) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 389, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 389, DateTimeKind.Local).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9952));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9491));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 93, DateTimeKind.Local).AddTicks(3572), "$2a$11$uWw5t65e1Q755r4MUGXnWuL9fwBy9bIb.UyFkFKtf4hT98GyN3Cce", new DateTime(2024, 7, 16, 11, 6, 52, 93, DateTimeKind.Local).AddTicks(4012) });
        }
    }
}
