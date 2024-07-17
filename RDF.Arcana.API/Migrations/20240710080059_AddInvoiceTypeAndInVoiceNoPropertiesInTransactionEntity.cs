using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceTypeAndInVoiceNoPropertiesInTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invoice_no",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "invoice_type",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(569), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(571) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(588), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(596) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(600), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(649) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(653), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(654) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(656), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(656) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(834));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(763));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(768));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 54, 621, DateTimeKind.Local).AddTicks(922), "$2a$11$AwF/x1nveyHkWtq1lXrvWutD8ifk5uT/kLiOFbuDxg6iQkBZig1zq", new DateTime(2024, 7, 10, 16, 0, 54, 621, DateTimeKind.Local).AddTicks(985) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invoice_no",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "invoice_type",
                table: "transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6910), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6933), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6935) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6942), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(7846) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(7995), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8002) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8009), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8590));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8603));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8467));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8474));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 39, 502, DateTimeKind.Local).AddTicks(9607), "$2a$11$T6MgD/qSX.JXMKH.v4nhGOqhJ9lLHaynZjaGp46925yZzoH4Jk/ru", new DateTime(2024, 7, 8, 8, 3, 39, 502, DateTimeKind.Local).AddTicks(9625) });
        }
    }
}
