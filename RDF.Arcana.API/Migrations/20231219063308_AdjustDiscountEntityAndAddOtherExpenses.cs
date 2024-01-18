using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDiscountEntityAndAddOtherExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "other_expenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expense_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_other_expenses", x => x.id);
                    table.ForeignKey(
                        name: "fk_other_expenses_users_added_by_user_id",
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
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1270), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1271) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1277), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1279) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1281), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1282) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1283), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1309), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1309) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1443));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1353));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1369));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 7, 420, DateTimeKind.Local).AddTicks(6215), "$2a$11$dOlcO7DCpuby88VvCNatN.YoubMaUQJaAcK9jQRATJtbnzyp7phm6" });

            migrationBuilder.CreateIndex(
                name: "ix_other_expenses_added_by",
                table: "other_expenses",
                column: "added_by",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "other_expenses");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6596), new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6599) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6605), new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6606) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6609), new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6612), new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6644) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6646), new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6647) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6842));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6720));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6791));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 12, 13, 46, 44, 317, DateTimeKind.Local).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 12, 13, 46, 43, 39, DateTimeKind.Local).AddTicks(6590), "$2a$11$IzR6MB2HeJ4rHYn3DCeaJ.fSJc2lXZDaUJjJS6z32V52EWwbNXaSa" });
        }
    }
}
