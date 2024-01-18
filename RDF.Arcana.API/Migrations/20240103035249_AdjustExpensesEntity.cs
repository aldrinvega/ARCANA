using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustExpensesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_expenses_request_id",
                table: "expenses");

            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5075), new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5077) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5081), new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5088) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5090), new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5108) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5110), new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5111) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5113), new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5114) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(5175));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 11, 52, 48, 124, DateTimeKind.Local).AddTicks(4950));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 11, 52, 47, 259, DateTimeKind.Local).AddTicks(7342), "$2a$11$G2K6XXc4XH5eHM3vgeodNeM09X.FjfzqCw2FxLdA3ejMWGyx7m1rG", new DateTime(2024, 1, 3, 11, 52, 47, 259, DateTimeKind.Local).AddTicks(7440) });

            migrationBuilder.CreateIndex(
                name: "ix_expenses_client_id",
                table: "expenses",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_request_id",
                table: "expenses",
                column: "request_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_expenses_clients_client_id",
                table: "expenses",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_expenses_clients_client_id",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "ix_expenses_client_id",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "ix_expenses_request_id",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "expenses");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5711), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5714) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5720), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5722) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5725), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5756) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5760), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5761) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5764), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5765) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5926));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5839));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5575));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 14, 336, DateTimeKind.Local).AddTicks(6817), "$2a$11$7e0eCjvPiITj9yo2FPeNMeDIZzY/LUxwtVCOAdXKsIF99XLVLXmhW", new DateTime(2023, 12, 29, 10, 36, 14, 336, DateTimeKind.Local).AddTicks(6849) });

            migrationBuilder.CreateIndex(
                name: "ix_expenses_request_id",
                table: "expenses",
                column: "request_id");
        }
    }
}
