using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvancePaymentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cash_advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cash_advance_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_cash_advance_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_cash_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_cash_advance_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cheque_advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cheque_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_cheque_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_cheque_advance_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "online_advance_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    advance_payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_online_advance_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_online_advance_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_online_advance_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_online_advance_payments_users_modifed_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9914), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9915) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9918), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9918) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9920), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9921) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9922), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9935) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9936), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9936) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 233, DateTimeKind.Local).AddTicks(117));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 233, DateTimeKind.Local).AddTicks(120));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9992));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9994));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 114, DateTimeKind.Local).AddTicks(4746), "$2a$11$CqJuH3MEaWPHsPHNiOY17uM7u1LX7ldA3P9RPeL6cuKvKDiqDioGK", new DateTime(2024, 3, 20, 14, 52, 16, 114, DateTimeKind.Local).AddTicks(4759) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_advance_payments");

            migrationBuilder.DropTable(
                name: "cheque_advance_payments");

            migrationBuilder.DropTable(
                name: "online_advance_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(446), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(446) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(450), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(451) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(452), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(452) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(454), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(469) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(571), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(572) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(680));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(684));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(614));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(644));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(355));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 287, DateTimeKind.Local).AddTicks(7151), "$2a$11$qBA/EIzxvWahpfhA62fGO.CuJ.xsNuiBLC01SXO59a7QZrKOOJLMW", new DateTime(2024, 3, 19, 11, 34, 46, 287, DateTimeKind.Local).AddTicks(7165) });
        }
    }
}
