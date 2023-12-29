using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchandisingAllowanceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "merchandising_allowances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    listing_fee_id = table.Column<int>(type: "int", nullable: false),
                    request_id = table.Column<int>(type: "int", nullable: false),
                    allowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
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
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 27, 8, 58, 50, 868, DateTimeKind.Local).AddTicks(7129), "$2a$11$od0sNexUaWbusHJBbPEuHuHzUnQdkRNMuRYaWj4/uda7KjI2Hb14." });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchandising_allowances");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4301), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4301) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4305), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4306) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4308), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4309) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4311), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4311) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4326), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4326) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4367));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4387));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4205));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 43, DateTimeKind.Local).AddTicks(3603), "$2a$11$DS9wSDjAI3/jOrx53EsCEO3ldStWdLww5f8RfHqLf8g8hez3QVC02" });
        }
    }
}
