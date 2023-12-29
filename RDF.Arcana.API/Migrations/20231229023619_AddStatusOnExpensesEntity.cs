using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusOnExpensesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5711), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5714) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5720), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5722) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5725), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5756) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5760), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5761) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5764), new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5765) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5926));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5839));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 29, 10, 36, 15, 340, DateTimeKind.Local).AddTicks(5575));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 29, 10, 36, 14, 336, DateTimeKind.Local).AddTicks(6817), "$2a$11$7e0eCjvPiITj9yo2FPeNMeDIZzY/LUxwtVCOAdXKsIF99XLVLXmhW", new DateTime(2023, 12, 29, 10, 36, 14, 336, DateTimeKind.Local).AddTicks(6849) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "expenses");

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
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 28, 16, 32, 45, 989, DateTimeKind.Local).AddTicks(6244), "$2a$11$2wM/0eg4PA3i/.zzlqDGi.F7vAntP0pEXOyztf..5We/JkOe.hUie", new DateTime(2023, 12, 28, 16, 32, 45, 989, DateTimeKind.Local).AddTicks(6341) });
        }
    }
}
