using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesInvoiceonSalesTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sales_invoice",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9182), new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9183) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9186), new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9187) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9189), new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9190) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9191), new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9208) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9209), new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9314));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9278));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9280));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 11, 4, 31, 70, DateTimeKind.Local).AddTicks(9086));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 4, 30, 940, DateTimeKind.Local).AddTicks(1269), "$2a$11$nbGV5dqdGX7i5wNzwcLaz.rrHVOuyOk/E2KMRzF5CLJJMQBjYl4LG", new DateTime(2024, 3, 26, 11, 4, 30, 940, DateTimeKind.Local).AddTicks(1283) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sales_invoice",
                table: "transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9345), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9346) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9349), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9352), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9353) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9354), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9378), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9378) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9415));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9434));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 443, DateTimeKind.Local).AddTicks(4143), "$2a$11$3usk4nUR7elREr/r1avoJ.q4nQscL3qKbYea9ZOwBsRbaCttnwB4O", new DateTime(2024, 3, 26, 10, 39, 52, 443, DateTimeKind.Local).AddTicks(4158) });
        }
    }
}
