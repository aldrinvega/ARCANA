using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipFromPaymentRecordsToPaymentTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "payment_record_id",
                table: "payment_transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9247), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9251) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9259), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9264), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9265) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9268), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9298), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9552));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9428));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(8719));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 284, DateTimeKind.Local).AddTicks(4057), "$2a$11$ZHOtLf9oSur9/FdVCX13HOcVnerTxqX9AmZDBFsgOZLjOHCkBC7HC", new DateTime(2024, 4, 18, 13, 49, 18, 284, DateTimeKind.Local).AddTicks(4073) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_payment_record_id",
                table: "payment_transactions",
                column: "payment_record_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_transactions_payment_records_payment_record_id",
                table: "payment_transactions",
                column: "payment_record_id",
                principalTable: "payment_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_transactions_payment_records_payment_record_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_payment_transactions_payment_record_id",
                table: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "payment_record_id",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(920), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(927) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(943), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(946) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(954), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(998) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1005), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1008) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1014), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1017) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1300));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1314));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1202));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(658));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 11, 331, DateTimeKind.Local).AddTicks(617), "$2a$11$UzVPXvYY7EGV4jBe5DJIHu1aYU9wFaI7QYc.iZLjBGYFcQZWvEsHu", new DateTime(2024, 4, 18, 13, 22, 11, 331, DateTimeKind.Local).AddTicks(637) });
        }
    }
}
