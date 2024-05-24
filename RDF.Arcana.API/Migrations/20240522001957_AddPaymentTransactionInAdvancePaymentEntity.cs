using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTransactionInAdvancePaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_transactions_transactions_transactions_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_payment_transactions_transactions_id",
                table: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "transactions_id",
                table: "payment_transactions");

            migrationBuilder.AddColumn<int>(
                name: "payment_transaction_id",
                table: "advance_payments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3616), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3621) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3634), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3635) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3638), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3667) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3673), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3680), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3681) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3892));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3902));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 54, 378, DateTimeKind.Local).AddTicks(992), "$2a$11$.7Xk0PrCgzUSEy2W88VRSe7jl8epmeDCdzCh7E5m/NB14H7j9LKka", new DateTime(2024, 5, 22, 8, 19, 54, 378, DateTimeKind.Local).AddTicks(1082) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_transaction_id",
                table: "payment_transactions",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id");

            migrationBuilder.AddForeignKey(
                name: "fk_advance_payments_payment_transactions_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id",
                principalTable: "payment_transactions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_transactions_transactions_transaction_id",
                table: "payment_transactions",
                column: "transaction_id",
                principalTable: "transactions",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_advance_payments_payment_transactions_payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.DropForeignKey(
                name: "fk_payment_transactions_transactions_transaction_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_payment_transactions_transaction_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.DropColumn(
                name: "payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.AddColumn<int>(
                name: "transactions_id",
                table: "payment_transactions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(713), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(714) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(718), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(719) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(720), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(721) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(722), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(741), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(741) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(833));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(784));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(800));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(801));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(628));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 694, DateTimeKind.Local).AddTicks(5171), "$2a$11$1R97EBJYRWtlGYrl/dvvW.97.P6m2kyPLSuLlasydERfbQ45xfQsC", new DateTime(2024, 4, 25, 15, 44, 11, 694, DateTimeKind.Local).AddTicks(5186) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_transactions_id",
                table: "payment_transactions",
                column: "transactions_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_transactions_transactions_transactions_id",
                table: "payment_transactions",
                column: "transactions_id",
                principalTable: "transactions",
                principalColumn: "id");
        }
    }
}
