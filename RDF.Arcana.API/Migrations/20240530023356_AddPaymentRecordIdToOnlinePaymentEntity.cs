using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentRecordIdToOnlinePaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "payment_record_id",
                table: "online_payments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8687), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8690) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8698), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8699) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8701), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8701) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8703), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8719) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8721), new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8722) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8773));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8795));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 30, 10, 33, 54, 172, DateTimeKind.Local).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 30, 10, 33, 53, 885, DateTimeKind.Local).AddTicks(7252), "$2a$11$7Vby/msr10e5q7H5MfNyzuAFTj74uPYvxaRe78D1ZqCG.t/NV5MtG", new DateTime(2024, 5, 30, 10, 33, 53, 885, DateTimeKind.Local).AddTicks(7416) });

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_payment_record_id",
                table: "online_payments",
                column: "payment_record_id");

            migrationBuilder.AddForeignKey(
                name: "fk_online_payments_payment_records_payment_record_id",
                table: "online_payments",
                column: "payment_record_id",
                principalTable: "payment_records",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_online_payments_payment_records_payment_record_id",
                table: "online_payments");

            migrationBuilder.DropIndex(
                name: "ix_online_payments_payment_record_id",
                table: "online_payments");

            migrationBuilder.DropColumn(
                name: "payment_record_id",
                table: "online_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1940), new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1941) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1944), new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1945) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1947), new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1947) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1949), new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1962) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1964), new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1964) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(2066));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(2070));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(2003));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(2031));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 28, 14, 52, 54, 363, DateTimeKind.Local).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 28, 14, 52, 54, 86, DateTimeKind.Local).AddTicks(5304), "$2a$11$7JdGZ4RTFTlPOC/JmfThQeR8lyqKpMd1T69r1ao/rceSY21ai20LO", new DateTime(2024, 5, 28, 14, 52, 54, 86, DateTimeKind.Local).AddTicks(5388) });
        }
    }
}
