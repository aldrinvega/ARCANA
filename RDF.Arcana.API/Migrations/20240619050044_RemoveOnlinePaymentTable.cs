using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOnlinePaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "online_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3205), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3206) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3223), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3223) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3225), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3281), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3282) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3283), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3284) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3942));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3452));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3795));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(2575));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 39, 862, DateTimeKind.Local).AddTicks(7855), "$2a$11$FEh9awJSqOOSODatp7wNPOLX5p3dFxVdGLZ9/GPMB27jWcD4XI40.", new DateTime(2024, 6, 19, 13, 0, 39, 862, DateTimeKind.Local).AddTicks(7880) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "online_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    payment_record_id = table.Column<int>(type: "int", nullable: true),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    online_payment_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    reference_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_online_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_online_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_online_payments_payment_records_payment_record_id",
                        column: x => x.payment_record_id,
                        principalTable: "payment_records",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_online_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_online_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3853), new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3865), new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3866) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3867), new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3882) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3885), new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3886) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3887), new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3888) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3992));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3996));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3938));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3956));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3958));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 14, 11, 22, 4, 167, DateTimeKind.Local).AddTicks(3615));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 14, 11, 22, 3, 247, DateTimeKind.Local).AddTicks(1926), "$2a$11$.iT0TEll50X79L2tSpCRRuc0Vy8sCCDxozDcB/O5HIodKIZBhAIHW", new DateTime(2024, 6, 14, 11, 22, 3, 247, DateTimeKind.Local).AddTicks(1962) });

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_added_by",
                table: "online_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_client_id",
                table: "online_payments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_modified_by",
                table: "online_payments",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_payment_record_id",
                table: "online_payments",
                column: "payment_record_id");
        }
    }
}
