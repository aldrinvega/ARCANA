using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddATag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "a_tag",
                table: "cleared_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7964), new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7965) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7971), new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7971) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7973), new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7974) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7975), new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7989) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7991), new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7991) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(8032));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 49, 56, 881, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 49, 56, 721, DateTimeKind.Local).AddTicks(9511), "$2a$11$sPAqF6QhFr78OdNWer7Um.3sPHToruf2/DFP48pdo9RX47V2akUcG", new DateTime(2024, 7, 22, 13, 49, 56, 721, DateTimeKind.Local).AddTicks(9533) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "a_tag",
                table: "cleared_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1261), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1262) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1266), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1267) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1268), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1269) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1270), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1293), new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1294) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1327));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1352));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1353));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 22, 13, 18, 54, 730, DateTimeKind.Local).AddTicks(1176));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 18, 54, 555, DateTimeKind.Local).AddTicks(1624), "$2a$11$KwrlGqD2WOaWkwlczwT5sOTj1j3Fq7m0b0SM/VTvXvaiw7pOGNWQq", new DateTime(2024, 7, 22, 13, 18, 54, 555, DateTimeKind.Local).AddTicks(1644) });
        }
    }
}
