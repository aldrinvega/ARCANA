using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class addReasonToPaymentRecordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "payment_records",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "payment_records");

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
    }
}
