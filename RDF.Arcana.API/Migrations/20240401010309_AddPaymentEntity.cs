using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "balance",
                table: "transaction_sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "advance_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payment_transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_id = table.Column<int>(type: "int", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_amount_received = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cheque_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactions_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_transactions_transactions_transactions_id",
                        column: x => x.transactions_id,
                        principalTable: "transactions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_payment_transactions_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8226), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8227) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8231), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8232) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8233), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8234) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8235), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8254) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8256), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8256) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8476));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 356, DateTimeKind.Local).AddTicks(1075), "$2a$11$gePchTD6WfWy4YQo27GHQugeqA757kT8.vO/y56MGJkLjxmiKuuuC", new DateTime(2024, 4, 1, 9, 3, 7, 356, DateTimeKind.Local).AddTicks(1090) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_added_by",
                table: "payment_transactions",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_transactions_id",
                table: "payment_transactions",
                column: "transactions_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "balance",
                table: "transaction_sales");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "advance_payments");

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
        }
    }
}
