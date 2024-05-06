using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameBalanceToRemainingBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "balance",
                table: "transaction_sales",
                newName: "remaining_balance");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2494), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2498), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2499) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2501), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2501) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2503), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2521) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2522), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2523) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2612));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2616));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2559));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2579));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2581));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2402));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 17, 993, DateTimeKind.Local).AddTicks(6111), "$2a$11$NVudMDx9CsoOzwFLdpopKuEG1/u4.nWjn2z4K.THddmnHTg/UAE2C", new DateTime(2024, 4, 24, 10, 4, 17, 993, DateTimeKind.Local).AddTicks(6138) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "remaining_balance",
                table: "transaction_sales",
                newName: "balance");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4179), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4184) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4190), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4199), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4226) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4233), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4237) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4242), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4246) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4438));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4032));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 16, 874, DateTimeKind.Local).AddTicks(8329), "$2a$11$ekGt0ZKq4VL8tn.em/l1F.M84aP4oVxG9H767/UoJnwtYiBnnPjVi", new DateTime(2024, 4, 24, 9, 12, 16, 874, DateTimeKind.Local).AddTicks(8354) });
        }
    }
}
