using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableInCreatedAtOnlinePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "online_payments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7346), new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7346) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7349), new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7352), new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7356) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7358), new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7381), new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7382) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7549));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7452));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7485));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7487));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 24, 13, 28, 36, 473, DateTimeKind.Local).AddTicks(7159));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 24, 13, 28, 35, 945, DateTimeKind.Local).AddTicks(4884), "$2a$11$X2plBh6DuKINhV5rrQ/2yOXmWYeORW61.Rork8ZL6I8w/3lfOs5dq", new DateTime(2024, 6, 24, 13, 28, 35, 945, DateTimeKind.Local).AddTicks(4940) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "online_payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9167), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9168) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9175), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9188), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9192) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9199), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9263) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9273), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9275) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 191, DateTimeKind.Local).AddTicks(5274), "$2a$11$nkXiBvpZDZquBwNUjP4pr.aBSpjpJjKcdRrkzQF2nH8RN8EbbDe2m", new DateTime(2024, 6, 19, 14, 2, 43, 191, DateTimeKind.Local).AddTicks(5357) });
        }
    }
}
