using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusOnAdvancePaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "advance_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9345), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9346) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9349), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9352), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9353) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9354), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9378), new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9378) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9415));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9434));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 26, 10, 39, 52, 567, DateTimeKind.Local).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 26, 10, 39, 52, 443, DateTimeKind.Local).AddTicks(4143), "$2a$11$3usk4nUR7elREr/r1avoJ.q4nQscL3qKbYea9ZOwBsRbaCttnwB4O", new DateTime(2024, 3, 26, 10, 39, 52, 443, DateTimeKind.Local).AddTicks(4158) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "advance_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2181), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2182) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2187), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2188) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2189), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2191), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2211) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2213), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2213) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2333));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2077));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 469, DateTimeKind.Local).AddTicks(111), "$2a$11$qY6DZfbvHwa.ahZkWg25Xe8hDk0Ewdw3y7Ion91fIedkR9eSVVogW", new DateTime(2024, 3, 24, 20, 13, 34, 469, DateTimeKind.Local).AddTicks(126) });
        }
    }
}
