using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameChareInvoiceNoToRemarksInTransactionSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "charge_invoice_no",
                table: "transaction_sales",
                newName: "remarks");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7911), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7911) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7914), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7915) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7916), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7917) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7918), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7933) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7935), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7936) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(8025));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(8029));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 71, DateTimeKind.Local).AddTicks(3363), "$2a$11$5rXJHlGmlweEZ0b82ensXup6DiYKOKaJ5Jg70M59R0nqwqAalMtHy", new DateTime(2024, 7, 26, 9, 51, 29, 71, DateTimeKind.Local).AddTicks(3394) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "remarks",
                table: "transaction_sales",
                newName: "charge_invoice_no");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(129), new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(134), new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(135) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(136), new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(137) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(138), new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(160) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(162), new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(162) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(267));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(202));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(226));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(228));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 23, 22, 9, 39, 463, DateTimeKind.Local).AddTicks(48));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 23, 22, 9, 39, 312, DateTimeKind.Local).AddTicks(2561), "$2a$11$lbjNsh.en.5DIhqMrc2HiObL4.xugX6elKhj4BI0KF3th07vKn2DK", new DateTime(2024, 7, 23, 22, 9, 39, 312, DateTimeKind.Local).AddTicks(2577) });
        }
    }
}
