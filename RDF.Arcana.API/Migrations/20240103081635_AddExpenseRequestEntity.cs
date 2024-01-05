using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_expenses_other_expenses_other_expenses_id",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "ix_expenses_other_expenses_id",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "other_expenses_id",
                table: "expenses");

            migrationBuilder.CreateTable(
                name: "expenses_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expenses_id = table.Column<int>(type: "int", nullable: false),
                    other_expense_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expenses_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_expenses_requests_expenses_expenses_id",
                        column: x => x.expenses_id,
                        principalTable: "expenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_expenses_requests_other_expenses_other_expense_id",
                        column: x => x.other_expense_id,
                        principalTable: "other_expenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1767), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1769) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1777), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1779) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1783), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1804) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1809), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1814), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1982));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1928));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1627));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 28, 245, DateTimeKind.Local).AddTicks(1430), "$2a$11$ljJd0uxaJmJ6m..0fxUNN..QMQb6/2b9.Hcp1U4Oi3ZSKRcmu4/O.", new DateTime(2024, 1, 3, 16, 16, 28, 245, DateTimeKind.Local).AddTicks(1465) });

            migrationBuilder.CreateIndex(
                name: "ix_expenses_requests_expenses_id",
                table: "expenses_requests",
                column: "expenses_id");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_requests_other_expense_id",
                table: "expenses_requests",
                column: "other_expense_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenses_requests");

            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "expenses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "other_expenses_id",
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
                name: "ix_expenses_other_expenses_id",
                table: "expenses",
                column: "other_expenses_id");

            migrationBuilder.AddForeignKey(
                name: "fk_expenses_other_expenses_other_expenses_id",
                table: "expenses",
                column: "other_expenses_id",
                principalTable: "other_expenses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
