using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableToMinMaxPropertiesInApproverByRangeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "min_value",
                table: "approver_by_range",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "max_value",
                table: "approver_by_range",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8871), new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8875) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8882), new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8882) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8886), new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8904) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8909), new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8912), new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8912) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(9054));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(9063));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8989));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(9005));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 11, 15, 55, 53, 667, DateTimeKind.Local).AddTicks(8749));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 11, 15, 55, 52, 468, DateTimeKind.Local).AddTicks(3343), "$2a$11$VnIV/EDDiLl1ie91EqWlOupF/InciNtKHs5k7iTrtrG45SHs.oJAO", new DateTime(2024, 7, 11, 15, 55, 52, 468, DateTimeKind.Local).AddTicks(3364) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "min_value",
                table: "approver_by_range",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "max_value",
                table: "approver_by_range",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(569), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(571) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(588), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(596) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(600), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(649) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(653), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(654) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(656), new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(656) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(834));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(763));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(768));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 10, 16, 0, 55, 340, DateTimeKind.Local).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 16, 0, 54, 621, DateTimeKind.Local).AddTicks(922), "$2a$11$AwF/x1nveyHkWtq1lXrvWutD8ifk5uT/kLiOFbuDxg6iQkBZig1zq", new DateTime(2024, 7, 10, 16, 0, 54, 621, DateTimeKind.Local).AddTicks(985) });
        }
    }
}
