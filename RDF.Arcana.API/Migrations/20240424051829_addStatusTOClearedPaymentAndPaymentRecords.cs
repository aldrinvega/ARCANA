using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class addStatusTOClearedPaymentAndPaymentRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "payment_records",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "cleared_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6394), new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6404), new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6404) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6407), new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6408) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6410), new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6434) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6437), new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6438) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6570));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6496));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6525));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 13, 18, 26, 128, DateTimeKind.Local).AddTicks(6016));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 13, 18, 25, 699, DateTimeKind.Local).AddTicks(593), "$2a$11$GpoqKu.XwQDlBfyQQ0z9M.icJQ/wVru8oEAYJE1rLe6LHX76m5RZ.", new DateTime(2024, 4, 24, 13, 18, 25, 699, DateTimeKind.Local).AddTicks(615) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "payment_records");

            migrationBuilder.DropColumn(
                name: "status",
                table: "cleared_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2494), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2498), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2499) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2501), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2501) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2503), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2521) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2522), new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2523) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2612));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2616));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2559));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2579));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2581));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 10, 4, 18, 313, DateTimeKind.Local).AddTicks(2402));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 10, 4, 17, 993, DateTimeKind.Local).AddTicks(6111), "$2a$11$NVudMDx9CsoOzwFLdpopKuEG1/u4.nWjn2z4K.THddmnHTg/UAE2C", new DateTime(2024, 4, 24, 10, 4, 17, 993, DateTimeKind.Local).AddTicks(6138) });
        }
    }
}
