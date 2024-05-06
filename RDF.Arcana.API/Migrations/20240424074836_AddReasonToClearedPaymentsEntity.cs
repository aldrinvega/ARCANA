using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonToClearedPaymentsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cleared_payments_id",
                table: "payment_records",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "cleared_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(859), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(862) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(866), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(867) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(870), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(889) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(892), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(893) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(895), new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(896) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(953));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 15, 48, 32, 582, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 48, 32, 78, DateTimeKind.Local).AddTicks(4534), "$2a$11$S8zGtEVzGrHmnuVGdFQhHew3xBT4k7Pn2O/vRu7pDp1yyIfvsC1Se", new DateTime(2024, 4, 24, 15, 48, 32, 78, DateTimeKind.Local).AddTicks(4554) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_records_cleared_payments_id",
                table: "payment_records",
                column: "cleared_payments_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_records_cleared_payments_cleared_payments_id",
                table: "payment_records",
                column: "cleared_payments_id",
                principalTable: "cleared_payments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_records_cleared_payments_cleared_payments_id",
                table: "payment_records");

            migrationBuilder.DropIndex(
                name: "ix_payment_records_cleared_payments_id",
                table: "payment_records");

            migrationBuilder.DropColumn(
                name: "cleared_payments_id",
                table: "payment_records");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "cleared_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7265), new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7266) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7271), new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7272) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7274), new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7296) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7298), new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7299) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7301), new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7302) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7362));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7364));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 14, 22, 12, 445, DateTimeKind.Local).AddTicks(7013));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 14, 22, 11, 741, DateTimeKind.Local).AddTicks(9710), "$2a$11$CUJQfnk2RNKd8/qCTRmbc.I6UnncRdyVFmACPuWNQwa0xTEuSlqKa", new DateTime(2024, 4, 24, 14, 22, 11, 741, DateTimeKind.Local).AddTicks(9734) });
        }
    }
}
