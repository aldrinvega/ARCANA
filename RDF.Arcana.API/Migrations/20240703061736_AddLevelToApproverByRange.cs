using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelToApproverByRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "level",
                table: "approver_by_range",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5160), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5161) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5165), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5165) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5167), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5179) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5181), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5182) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5184), new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5185) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5282));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5286));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5221));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 3, 14, 17, 32, 829, DateTimeKind.Local).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 17, 32, 410, DateTimeKind.Local).AddTicks(2220), "$2a$11$ZkpivTjauG6VvYyPqyz2FOPudtbiTQ0tx3F2nFbxxPRksWJpJTYaG", new DateTime(2024, 7, 3, 14, 17, 32, 410, DateTimeKind.Local).AddTicks(2276) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "level",
                table: "approver_by_range");

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
    }
}
