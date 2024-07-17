using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnTransactionsReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9899), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9901) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9904), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9904) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9905), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9918), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9918) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9919), new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9920) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 389, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 389, DateTimeKind.Local).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9952));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 16, 11, 6, 52, 388, DateTimeKind.Local).AddTicks(9491));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 16, 11, 6, 52, 93, DateTimeKind.Local).AddTicks(3572), "$2a$11$uWw5t65e1Q755r4MUGXnWuL9fwBy9bIb.UyFkFKtf4hT98GyN3Cce", new DateTime(2024, 7, 16, 11, 6, 52, 93, DateTimeKind.Local).AddTicks(4012) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3594), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3596) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3603), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3606) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3611), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3612) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3615), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3646) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3651), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3891));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3734));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3438));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 457, DateTimeKind.Local).AddTicks(6440), "$2a$11$W35SiPL1SESB0VlDAxPfm.1IdC9TtN5TLS3SLfgDe.zelsGGffKyS", new DateTime(2024, 7, 12, 13, 44, 6, 457, DateTimeKind.Local).AddTicks(6515) });
        }
    }
}
