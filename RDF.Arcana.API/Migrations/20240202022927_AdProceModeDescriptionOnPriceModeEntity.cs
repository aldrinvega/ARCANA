using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdProceModeDescriptionOnPriceModeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "price_mode_description",
                table: "price_mode",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7845), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7849) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7860), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7863) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7870), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7881) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7896), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7982) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7995), new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8003) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8253));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8336));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 10, 29, 26, 224, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 29, 25, 851, DateTimeKind.Local).AddTicks(8243), "$2a$11$e9tpvxPFzLoQQA4XmHkPxeNJH9f.YU8cRsVcBkoxxAVMDFOFxomgi", new DateTime(2024, 2, 2, 10, 29, 25, 851, DateTimeKind.Local).AddTicks(8256) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_mode_description",
                table: "price_mode");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9812), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9813) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9816), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9817) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9818), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9819) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9820), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9832) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9833), new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9834) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9917));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9921));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9875));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 8, 18, 17, 721, DateTimeKind.Local).AddTicks(9719));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 8, 18, 17, 451, DateTimeKind.Local).AddTicks(8765), "$2a$11$6bEr3Y7RoAclcLYZd2jPFOYa1cMiRnmahaT31ERWu4VD0trpDpbHq", new DateTime(2024, 2, 2, 8, 18, 17, 451, DateTimeKind.Local).AddTicks(8787) });
        }
    }
}
