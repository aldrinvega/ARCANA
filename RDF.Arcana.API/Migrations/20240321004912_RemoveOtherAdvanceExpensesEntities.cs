using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOtherAdvanceExpensesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_advance_payments");

            migrationBuilder.DropTable(
                name: "cheque_advance_payments");

            migrationBuilder.DropTable(
                name: "online_advance_payments");

            migrationBuilder.CreateTable(
                name: "advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cheque_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_advance_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_advance_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_advance_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(17), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(18) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(23), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(23) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(25), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(25) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(26), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(44) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(45), new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(46) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(90));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(111));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 624, DateTimeKind.Local).AddTicks(113));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 21, 8, 49, 11, 623, DateTimeKind.Local).AddTicks(9919));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 8, 49, 11, 463, DateTimeKind.Local).AddTicks(7337), "$2a$11$cZiZmp4zICv36yvondla8O94LNaKgBPPcXFhb/LKOdsDpSQFk.9UO", new DateTime(2024, 3, 21, 8, 49, 11, 463, DateTimeKind.Local).AddTicks(7353) });

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_added_by",
                table: "advance_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_client_id",
                table: "advance_payments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_advance_payments_modified_by",
                table: "advance_payments",
                column: "modified_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advance_payments");

            migrationBuilder.CreateTable(
                name: "cash_advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cash_advance_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_cash_advance_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cash_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cash_advance_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cheque_advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cheque_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cheque_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cheque_advance_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_cheque_advance_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cheque_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cheque_advance_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "online_advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_online_advance_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_online_advance_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_online_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_online_advance_payments_users_modifed_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3887), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3888) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3892), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3893) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3895), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3896) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3897), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3917), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3918) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(4031));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3962));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3992));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3994));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 206, DateTimeKind.Local).AddTicks(4879), "$2a$11$2RA9dPqod8V.gOL0BN27i.gmqYtxJMcd8FdHsxfAfkvTuMp3udYdm", new DateTime(2024, 3, 20, 16, 56, 29, 206, DateTimeKind.Local).AddTicks(4897) });

            migrationBuilder.CreateIndex(
                name: "ix_cash_advance_payments_added_by",
                table: "cash_advance_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_cash_advance_payments_client_id",
                table: "cash_advance_payments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_cash_advance_payments_modified_by",
                table: "cash_advance_payments",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_cheque_advance_payments_added_by",
                table: "cheque_advance_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_cheque_advance_payments_client_id",
                table: "cheque_advance_payments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_cheque_advance_payments_modified_by",
                table: "cheque_advance_payments",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_online_advance_payments_added_by",
                table: "online_advance_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_online_advance_payments_client_id",
                table: "online_advance_payments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_online_advance_payments_modified_by",
                table: "online_advance_payments",
                column: "modified_by");
        }
    }
}
