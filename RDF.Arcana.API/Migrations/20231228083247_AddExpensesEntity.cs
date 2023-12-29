using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddExpensesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchandising_allowances");

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    other_expenses_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    request_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expenses", x => x.id);
                    table.ForeignKey(
                        name: "fk_expenses_other_expenses_other_expenses_id",
                        column: x => x.other_expenses_id,
                        principalTable: "other_expenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_expenses_requests_request_id",
                        column: x => x.request_id,
                        principalTable: "requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_expenses_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_expenses_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2313), new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2315) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2321), new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2322) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2325), new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2326) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2329), new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2359) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2363), new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2364) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2515));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2457));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 28, 16, 32, 46, 346, DateTimeKind.Local).AddTicks(2177));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "is_active", "password", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 45, 989, DateTimeKind.Local).AddTicks(6244), true, "$2a$11$2wM/0eg4PA3i/.zzlqDGi.F7vAntP0pEXOyztf..5We/JkOe.hUie", new DateTime(2023, 12, 28, 16, 32, 45, 989, DateTimeKind.Local).AddTicks(6341) });

            migrationBuilder.CreateIndex(
                name: "ix_expenses_added_by",
                table: "expenses",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_modified_by",
                table: "expenses",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_other_expenses_id",
                table: "expenses",
                column: "other_expenses_id");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_request_id",
                table: "expenses",
                column: "request_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.CreateTable(
                name: "merchandising_allowances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    listing_fee_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    request_id = table.Column<int>(type: "int", nullable: false),
                    allowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchandising_allowances", x => x.id);
                    table.ForeignKey(
                        name: "fk_merchandising_allowances_listing_fees_listing_fee_id",
                        column: x => x.listing_fee_id,
                        principalTable: "listing_fees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_merchandising_allowances_requests_request_id",
                        column: x => x.request_id,
                        principalTable: "requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_merchandising_allowances_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_merchandising_allowances_users_modified_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6370), new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6371) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6375), new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6375) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6377), new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6377) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6378), new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6379) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6392), new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6392) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6458));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6461));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6430));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 27, 8, 58, 51, 110, DateTimeKind.Local).AddTicks(6283));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "is_active", "password", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 50, 868, DateTimeKind.Local).AddTicks(7129), false, "$2a$11$od0sNexUaWbusHJBbPEuHuHzUnQdkRNMuRYaWj4/uda7KjI2Hb14.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "ix_merchandising_allowances_added_by",
                table: "merchandising_allowances",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_merchandising_allowances_listing_fee_id",
                table: "merchandising_allowances",
                column: "listing_fee_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_merchandising_allowances_modified_by",
                table: "merchandising_allowances",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_merchandising_allowances_request_id",
                table: "merchandising_allowances",
                column: "request_id",
                unique: true);
        }
    }
}
