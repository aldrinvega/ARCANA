using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPropertyInExpensesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total",
                table: "expenses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6910), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6933), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6935) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6942), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(7846) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(7995), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8002) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8009), new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8590));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8603));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8467));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(8474));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 8, 8, 3, 40, 508, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 8, 8, 3, 39, 502, DateTimeKind.Local).AddTicks(9607), "$2a$11$T6MgD/qSX.JXMKH.v4nhGOqhJ9lLHaynZjaGp46925yZzoH4Jk/ru", new DateTime(2024, 7, 8, 8, 3, 39, 502, DateTimeKind.Local).AddTicks(9625) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total",
                table: "expenses");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5160), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5161) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5165), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5165) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5167), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5179) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5181), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5182) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5184), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5185) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5282));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5286));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5221));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 410, DateTimeKind.Local).AddTicks(2220), "$2a$11$ZkpivTjauG6VvYyPqyz2FOPudtbiTQ0tx3F2nFbxxPRksWJpJTYaG", new DateTime(2024, 7, 3, 14, 17, 32, 410, DateTimeKind.Local).AddTicks(2276) });
        }
    }
}
