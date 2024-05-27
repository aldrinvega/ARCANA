using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnlinePaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "online_method",
                table: "online_payments",
                newName: "reference_number");

            migrationBuilder.AddColumn<int>(
                name: "payment_transaction_id",
                table: "online_payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "payment_type",
                table: "online_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2733), new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2734) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2739), new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2742), new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2752) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2754), new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2755) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2757), new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2758) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(3377));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(3383));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(3199));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 11, 54, 16, 821, DateTimeKind.Local).AddTicks(2623));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 11, 54, 16, 235, DateTimeKind.Local).AddTicks(9703), "$2a$11$wXZPFmkYH9EJGDZmSM2bD.CkLJ6.8.TJ5t.Abo9vmMIc134.2ipnq", new DateTime(2024, 5, 27, 11, 54, 16, 235, DateTimeKind.Local).AddTicks(9721) });

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_payment_transaction_id",
                table: "online_payments",
                column: "payment_transaction_id");

            migrationBuilder.AddForeignKey(
                name: "fk_online_payments_payment_transactions_payment_transaction_id",
                table: "online_payments",
                column: "payment_transaction_id",
                principalTable: "payment_transactions",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_online_payments_payment_transactions_payment_transaction_id",
                table: "online_payments");

            migrationBuilder.DropIndex(
                name: "ix_online_payments_payment_transaction_id",
                table: "online_payments");

            migrationBuilder.DropColumn(
                name: "payment_transaction_id",
                table: "online_payments");

            migrationBuilder.DropColumn(
                name: "payment_type",
                table: "online_payments");

            migrationBuilder.RenameColumn(
                name: "reference_number",
                table: "online_payments",
                newName: "online_method");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5110), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5115) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5127), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5129) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5138), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5141) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5148), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5202) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5210), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5213) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5485));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5499));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5327));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 29, DateTimeKind.Local).AddTicks(3898), "$2a$11$7xNqUX/f1JG0iGO4Gk7yweGtTEZDW27qJFTM2A.IrH2SsBPwNsn0S", new DateTime(2024, 5, 27, 10, 51, 57, 29, DateTimeKind.Local).AddTicks(4022) });
        }
    }
}
