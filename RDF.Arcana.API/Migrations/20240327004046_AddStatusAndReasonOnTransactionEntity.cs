using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndReasonOnTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_transaction_sales_transaction_id",
                table: "transaction_sales");

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3266), new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3267) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3271), new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3272) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3274), new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3274) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3276), new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3295), new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3296) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3441));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3446));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3351));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3384));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 27, 8, 40, 45, 789, DateTimeKind.Local).AddTicks(3159));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 27, 8, 40, 45, 596, DateTimeKind.Local).AddTicks(4434), "$2a$11$/SE0KqmkPlg.eTgDU9po5eP4fDhS6XCkNR7Ih4FtlWyDiQTruvbMy", new DateTime(2024, 3, 27, 8, 40, 45, 596, DateTimeKind.Local).AddTicks(4453) });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_sales_transaction_id",
                table: "transaction_sales",
                column: "transaction_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_transaction_sales_transaction_id",
                table: "transaction_sales");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "status",
                table: "transactions");

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

            migrationBuilder.CreateIndex(
                name: "ix_transaction_sales_transaction_id",
                table: "transaction_sales",
                column: "transaction_id");
        }
    }
}
