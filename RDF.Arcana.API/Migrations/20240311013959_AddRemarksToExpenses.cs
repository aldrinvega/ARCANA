using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarksToExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "expenses_requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6164), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6168), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6168) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6170), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6171), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6188) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6189), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6291));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6295));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6255));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6256));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6043));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 43, DateTimeKind.Local).AddTicks(582), "$2a$11$i0dXEkVqJTFF4Ck8PsKoceqd066tSwZmD60LrNIPU8i2585XqVfhK", new DateTime(2024, 3, 11, 9, 39, 59, 43, DateTimeKind.Local).AddTicks(594) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remarks",
                table: "expenses_requests");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8427), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8428) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8432), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8432) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8436), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8437) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8438), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8455) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8456), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8456) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8551));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8555));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8518));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8520));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 834, DateTimeKind.Local).AddTicks(5130), "$2a$11$Io4mICB/KRwTiI3mCRcZIOUBZMBLSVz3AawptrQF8Xdyr4SKmQ1Qa", new DateTime(2024, 2, 21, 11, 24, 35, 834, DateTimeKind.Local).AddTicks(5144) });
        }
    }
}
