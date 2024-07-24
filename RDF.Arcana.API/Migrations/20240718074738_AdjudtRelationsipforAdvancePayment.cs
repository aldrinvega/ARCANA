using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjudtRelationsipforAdvancePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2738), new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2738) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2744), new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2745) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2746), new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2747) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2762), new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2763) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2764), new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2773) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2869));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2873));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 18, 15, 47, 35, 535, DateTimeKind.Local).AddTicks(2551));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 18, 15, 47, 35, 383, DateTimeKind.Local).AddTicks(1591), "$2a$11$1OX5kXiWjsCU1t/sr0D7puVwN1B2VEYR59zflzLc22CEyA/3m9zwG", new DateTime(2024, 7, 18, 15, 47, 35, 383, DateTimeKind.Local).AddTicks(1611) });

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id",
                unique: true,
                filter: "[payment_transaction_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9899), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9901) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9904), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9904) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9905), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9918), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9918) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9919), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9920) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 389, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 389, DateTimeKind.Local).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9952));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9491));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 93, DateTimeKind.Local).AddTicks(3572), "$2a$11$uWw5t65e1Q755r4MUGXnWuL9fwBy9bIb.UyFkFKtf4hT98GyN3Cce", new DateTime(2024, 7, 16, 11, 6, 52, 93, DateTimeKind.Local).AddTicks(4012) });

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_payment_transaction_id",
                table: "advance_payments",
                column: "payment_transaction_id");
        }
    }
}
