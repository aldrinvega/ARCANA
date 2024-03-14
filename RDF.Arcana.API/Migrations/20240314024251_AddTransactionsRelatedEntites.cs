using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionsRelatedEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_transactions_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "transaction_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_transaction_items_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_transaction_items_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "transaction_sales",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_id = table.Column<int>(type: "int", nullable: false),
                    vatable_sales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vat_exempt_sales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    zero_rated_sales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vat_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_sales = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    amount_due = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    add_vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_amount_due = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    uopdated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_sales", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_sales_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_transaction_sales_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8087), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8088) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8092), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8093) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8094), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8094) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8097), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8122) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8124), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8124) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8233));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8237));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8166));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8198));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 67, DateTimeKind.Local).AddTicks(3040), "$2a$11$ks9CytQnHPy/NIy648xa5OsuzTb1Jcx1j7mZkR9Zn4o1C3RZxVZ7.", new DateTime(2024, 3, 14, 10, 42, 51, 67, DateTimeKind.Local).AddTicks(3054) });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_items_added_by",
                table: "transaction_items",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_items_item_id",
                table: "transaction_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_items_transaction_id",
                table: "transaction_items",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_sales_added_by",
                table: "transaction_sales",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_sales_transaction_id",
                table: "transaction_sales",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_added_by",
                table: "transactions",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_client_id",
                table: "transactions",
                column: "client_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction_items");

            migrationBuilder.DropTable(
                name: "transaction_sales");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1125), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1125) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1129), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1129) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1130), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1131) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1132), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1373) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1381), new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1381) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1467));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 13, 9, 28, 22, 903, DateTimeKind.Local).AddTicks(1041));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 13, 9, 28, 22, 784, DateTimeKind.Local).AddTicks(1668), "$2a$11$h98qeG0Qiwx5hPV1ji/CqOgl9WrNKafX8kMr3kQ9hV0s3OBtpvPpa", new DateTime(2024, 3, 13, 9, 28, 22, 784, DateTimeKind.Local).AddTicks(1683) });
        }
    }
}
