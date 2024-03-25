using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdditemImageLinkToItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "item_image_link",
                table: "items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2181), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2182) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2187), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2188) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2189), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2191), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2211) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2213), new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2213) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2333));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 13, 34, 630, DateTimeKind.Local).AddTicks(2077));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 13, 34, 469, DateTimeKind.Local).AddTicks(111), "$2a$11$qY6DZfbvHwa.ahZkWg25Xe8hDk0Ewdw3y7Ion91fIedkR9eSVVogW", new DateTime(2024, 3, 24, 20, 13, 34, 469, DateTimeKind.Local).AddTicks(126) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "item_image_link",
                table: "items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3537), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3537) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3542), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3542) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3544), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3544) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3546), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3556) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3558), new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3558) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3751));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3696));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 24, 20, 0, 49, 177, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 24, 20, 0, 49, 31, DateTimeKind.Local).AddTicks(3848), "$2a$11$EVDu1Lx6JlKrDjVlZJMQPelIcaK3izoEJksWVZY8Ry4/BC/HY9beC", new DateTime(2024, 3, 24, 20, 0, 49, 31, DateTimeKind.Local).AddTicks(3864) });
        }
    }
}
