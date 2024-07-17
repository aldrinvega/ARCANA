using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddApproverSetupBasedOnRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "approver_by_range",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    module_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    min_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    max_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approver_by_range", x => x.id);
                    table.ForeignKey(
                        name: "fk_approver_by_range_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ix_approver_by_range_user_id",
                table: "approver_by_range",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approver_by_range");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9425), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9435), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9436) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9439), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9441) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9445), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9467) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9471), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9473) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9620));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9565));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 23, 535, DateTimeKind.Local).AddTicks(1899), "$2a$11$pu2GozJn7//laB/oyeoS2.cHlVkwIS85zx8Np0YDdmU.guz1IeY4O", new DateTime(2024, 6, 10, 12, 54, 23, 535, DateTimeKind.Local).AddTicks(1939) });
        }
    }
}
