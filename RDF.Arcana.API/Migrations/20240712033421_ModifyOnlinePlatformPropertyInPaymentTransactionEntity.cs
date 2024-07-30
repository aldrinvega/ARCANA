using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyOnlinePlatformPropertyInPaymentTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "online_platform",
                table: "payment_transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7633), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7634) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7638), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7639) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7641), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7642) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7644), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7661) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7663), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7664) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7769));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7776));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7733));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7544));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 17, 873, DateTimeKind.Local).AddTicks(9517), "$2a$11$qrV9Pn/A3Js8PInBI7u5huvnLAf9zUYp3yQyM/kH.HhUxuNAD.ILO", new DateTime(2024, 7, 12, 11, 34, 17, 873, DateTimeKind.Local).AddTicks(9536) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "online_platform",
                table: "payment_transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
