using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOnlinePaymentCredentialsOnPaymentTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "online_platform",
                table: "payment_transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reference_no",
                table: "payment_transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4453), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4455) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4466), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4467) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4470), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4471) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4473), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4531) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4537), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4538) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4923));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4934));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4670));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 341, DateTimeKind.Local).AddTicks(2003), "$2a$11$X4WDIDTEIfY8Q62KTrwaoePq3Hb3IIKKbpXKZ7VaVMoUYFQbD8K3.", new DateTime(2024, 5, 31, 13, 24, 50, 341, DateTimeKind.Local).AddTicks(2072) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "online_platform",
                table: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "reference_no",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8687), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8690) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8698), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8699) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8701), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8701) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8703), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8719) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8721), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8722) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8773));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8795));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 53, 885, DateTimeKind.Local).AddTicks(7252), "$2a$11$7Vby/msr10e5q7H5MfNyzuAFTj74uPYvxaRe78D1ZqCG.t/NV5MtG", new DateTime(2024, 5, 30, 10, 33, 53, 885, DateTimeKind.Local).AddTicks(7416) });
        }
    }
}
