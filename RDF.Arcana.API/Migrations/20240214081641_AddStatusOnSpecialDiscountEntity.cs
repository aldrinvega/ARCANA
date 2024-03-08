using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusOnSpecialDiscountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "special_discounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4262), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4263) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4266), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4266) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4268), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4268) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4270), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4281) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4282), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4283) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4318));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 407, DateTimeKind.Local).AddTicks(5657), "$2a$11$yBePLrnLxriKoDuGxSIkA.lQKBEKY6lbQHJJm0ZcQsqHgq1k/Exe6", new DateTime(2024, 2, 14, 16, 16, 41, 407, DateTimeKind.Local).AddTicks(5670) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "special_discounts");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8853), new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8854) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8858), new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8858) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8859), new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8859) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8860), new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8873) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8875), new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8876) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8965));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8938));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 13, 56, 14, 851, DateTimeKind.Local).AddTicks(8771));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 13, 56, 14, 735, DateTimeKind.Local).AddTicks(5073), "$2a$11$HNZYMzaLLG3xCNVwUAmmKut98gmb3hizs6pmu48lfXUEhE8eptACG", new DateTime(2024, 2, 14, 13, 56, 14, 735, DateTimeKind.Local).AddTicks(5085) });
        }
    }
}
