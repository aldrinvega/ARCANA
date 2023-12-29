using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustAddedByOnOtherExpensesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_other_expenses_added_by",
                table: "other_expenses");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9525), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9534), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9535) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9537), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9538) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9539), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9562) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9564), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9565) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9683));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9624));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9652));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 4, 606, DateTimeKind.Local).AddTicks(879), "$2a$11$Em5gsex4vTjjRVY61YgUSeAqYNp0TT/iJdZiuC2XPIQBsdJjcolgG" });

            migrationBuilder.CreateIndex(
                name: "ix_other_expenses_added_by",
                table: "other_expenses",
                column: "added_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_other_expenses_added_by",
                table: "other_expenses");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1565), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1568) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1576), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1577) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1580), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1581) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1585), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1603) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1605), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1605) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1723));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1647));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 477, DateTimeKind.Local).AddTicks(3834), "$2a$11$z1iBsW7UostN8asGtFZaNe7gwH100lNUH3XSHEZAEP0jRUMYgWMty" });

            migrationBuilder.CreateIndex(
                name: "ix_other_expenses_added_by",
                table: "other_expenses",
                column: "added_by",
                unique: true);
        }
    }
}
