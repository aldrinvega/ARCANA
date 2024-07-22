using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTransactionOnClearedPaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cleared_payments_payment_records_payment_record_id",
                table: "cleared_payments");

            migrationBuilder.DropIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.RenameColumn(
                name: "payment_record_id",
                table: "cleared_payments",
                newName: "payment_transaction_id");

            migrationBuilder.RenameIndex(
                name: "ix_cleared_payments_payment_record_id",
                table: "cleared_payments",
                newName: "ix_cleared_payments_payment_transaction_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1261), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1262) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1266), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1267) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1268), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1269) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1270), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1293), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1294) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1327));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1352));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1353));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1176));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 555, DateTimeKind.Local).AddTicks(1624), "$2a$11$KwrlGqD2WOaWkwlczwT5sOTj1j3Fq7m0b0SM/VTvXvaiw7pOGNWQq", new DateTime(2024, 7, 22, 13, 18, 54, 555, DateTimeKind.Local).AddTicks(1644) });

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id",
                unique: true,
                filter: "[payment_transaction_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "fk_cleared_payments_payment_transactions_payment_transaction_id",
                table: "cleared_payments",
                column: "payment_transaction_id",
                principalTable: "payment_transactions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cleared_payments_payment_transactions_payment_transaction_id",
                table: "cleared_payments");

            migrationBuilder.DropIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.RenameColumn(
                name: "payment_transaction_id",
                table: "cleared_payments",
                newName: "payment_record_id");

            migrationBuilder.RenameIndex(
                name: "ix_cleared_payments_payment_transaction_id",
                table: "cleared_payments",
                newName: "ix_cleared_payments_payment_record_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cleared_payments_payment_records_payment_record_id",
                table: "cleared_payments",
                column: "payment_record_id",
                principalTable: "payment_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
