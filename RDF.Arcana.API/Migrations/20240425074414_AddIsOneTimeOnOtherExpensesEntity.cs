using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsOneTimeOnOtherExpensesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_one_time",
                table: "expenses_requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(713), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(714) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(718), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(719) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(720), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(721) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(722), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(741), new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(741) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(833));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(784));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(800));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(801));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 25, 15, 44, 11, 841, DateTimeKind.Local).AddTicks(628));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 44, 11, 694, DateTimeKind.Local).AddTicks(5171), "$2a$11$1R97EBJYRWtlGYrl/dvvW.97.P6m2kyPLSuLlasydERfbQ45xfQsC", new DateTime(2024, 4, 25, 15, 44, 11, 694, DateTimeKind.Local).AddTicks(5186) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_one_time",
                table: "expenses_requests");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7128), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7131) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7148), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7149) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7158), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7159) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7162), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7226) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7229), new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 16, 9, 43, 404, DateTimeKind.Local).AddTicks(6857));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 16, 9, 42, 989, DateTimeKind.Local).AddTicks(8670), "$2a$11$RK/W0fPK1TDEGGA4wnQmSuZh7o4BttcimP4UQc3rkpXjHkflgG9ta", new DateTime(2024, 4, 24, 16, 9, 42, 989, DateTimeKind.Local).AddTicks(8693) });
        }
    }
}
