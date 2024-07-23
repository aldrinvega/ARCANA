using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddjustClearedPaymentsEntity : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "a_tag",
                table: "cleared_payments",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id",
                unique: false,
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

            migrationBuilder.DropColumn(
                name: "a_tag",
                table: "cleared_payments");

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
