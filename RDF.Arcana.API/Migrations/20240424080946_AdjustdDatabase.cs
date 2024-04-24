using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustdDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_records_cleared_payments_cleared_payments_id",
                table: "payment_records");

            migrationBuilder.DropForeignKey(
                name: "fk_payment_transactions_cleared_payments_cleared_payments_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_payment_transactions_cleared_payments_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_payment_records_cleared_payments_id",
                table: "payment_records");

            migrationBuilder.DropColumn(
                name: "cleared_payments_id",
                table: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "cleared_payments_id",
                table: "payment_records");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7128), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7131) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7148), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7149) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7158), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7159) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7162), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7226) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7229), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(6857));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 42, 989, DateTimeKind.Local).AddTicks(8670), "$2a$11$RK/W0fPK1TDEGGA4wnQmSuZh7o4BttcimP4UQc3rkpXjHkflgG9ta", new DateTime(2024, 4, 24, 16, 9, 42, 989, DateTimeKind.Local).AddTicks(8693) });

            migrationBuilder.CreateIndex(
                name: "ix_cleared_payments_payment_record_id",
                table: "cleared_payments",
                column: "payment_record_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_cleared_payments_payment_records_payment_record_id",
                table: "cleared_payments",
                column: "payment_record_id",
                principalTable: "payment_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cleared_payments_payment_records_payment_record_id",
                table: "cleared_payments");

            migrationBuilder.DropIndex(
                name: "ix_cleared_payments_payment_record_id",
                table: "cleared_payments");

            migrationBuilder.AddColumn<int>(
                name: "cleared_payments_id",
                table: "payment_transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cleared_payments_id",
                table: "payment_records",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(859), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(862) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(866), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(867) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(870), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(889) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(892), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(893) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(895), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(896) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(953));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 78, DateTimeKind.Local).AddTicks(4534), "$2a$11$S8zGtEVzGrHmnuVGdFQhHew3xBT4k7Pn2O/vRu7pDp1yyIfvsC1Se", new DateTime(2024, 4, 24, 15, 48, 32, 78, DateTimeKind.Local).AddTicks(4554) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_cleared_payments_id",
                table: "payment_transactions",
                column: "cleared_payments_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_records_cleared_payments_id",
                table: "payment_records",
                column: "cleared_payments_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_records_cleared_payments_cleared_payments_id",
                table: "payment_records",
                column: "cleared_payments_id",
                principalTable: "cleared_payments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_transactions_cleared_payments_cleared_payments_id",
                table: "payment_transactions",
                column: "cleared_payments_id",
                principalTable: "cleared_payments",
                principalColumn: "id");
        }
    }
}
