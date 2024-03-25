using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingBalanceForAdvancePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_balance",
                table: "advance_payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3537), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3537) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3542), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3542) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3544), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3544) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3546), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3556) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3558), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3558) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3751));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3696));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 31, DateTimeKind.Local).AddTicks(3848), "$2a$11$EVDu1Lx6JlKrDjVlZJMQPelIcaK3izoEJksWVZY8Ry4/BC/HY9beC", new DateTime(2024, 3, 24, 20, 0, 49, 31, DateTimeKind.Local).AddTicks(3864) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remaining_balance",
                table: "advance_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(17), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(18) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(23), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(23) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(25), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(25) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(26), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(44) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(45), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(46) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(90));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(111));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(113));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 623, DateTimeKind.Local).AddTicks(9919));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 463, DateTimeKind.Local).AddTicks(7337), "$2a$11$cZiZmp4zICv36yvondla8O94LNaKgBPPcXFhb/LKOdsDpSQFk.9UO", new DateTime(2024, 3, 21, 8, 49, 11, 463, DateTimeKind.Local).AddTicks(7353) });
        }
    }
}
