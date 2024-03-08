using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItemsIdToPriceChangeItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_item_price_changes_items_items_id",
                table: "item_price_changes");

            migrationBuilder.DropIndex(
                name: "ix_item_price_changes_items_id",
                table: "item_price_changes");

            migrationBuilder.DropColumn(
                name: "items_id",
                table: "item_price_changes");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3028), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3029) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3033), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3033) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3036), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3037) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3038), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3059) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3061), new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3062) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3179));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3111));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3137));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(3139));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 18, 1, 14, 418, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 18, 1, 14, 85, DateTimeKind.Local).AddTicks(6199), "$2a$11$P0RdDKp0WHd5Xwz2uYa97u70Z1N7IFmKM1CFmATzJv.Tfk6RSIbwy", new DateTime(2024, 2, 3, 18, 1, 14, 85, DateTimeKind.Local).AddTicks(6312) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "items_id",
                table: "item_price_changes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9089), new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9092) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9097), new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9098) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9102), new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9103) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9105), new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9133), new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9134) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9276));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9282));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9194));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(9226));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 16, 44, 55, 996, DateTimeKind.Local).AddTicks(8960));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 16, 44, 55, 575, DateTimeKind.Local).AddTicks(848), "$2a$11$1SsCzUIULfC8R4g6XoygT.u0LAT.ueDrROghImPZ99hdRzxKhjGqG", new DateTime(2024, 2, 3, 16, 44, 55, 575, DateTimeKind.Local).AddTicks(865) });

            migrationBuilder.CreateIndex(
                name: "ix_item_price_changes_items_id",
                table: "item_price_changes",
                column: "items_id");

            migrationBuilder.AddForeignKey(
                name: "fk_item_price_changes_items_items_id",
                table: "item_price_changes",
                column: "items_id",
                principalTable: "items",
                principalColumn: "id");
        }
    }
}
