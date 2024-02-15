using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class NullableModifiedByInSpDiscountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_special_discounts_users_modified_by_user_id",
                table: "special_discounts");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "special_discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "fk_special_discounts_users_modified_by_user_id",
                table: "special_discounts",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_special_discounts_users_modified_by_user_id",
                table: "special_discounts");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "special_discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(877), new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(877) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(881), new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(882) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(883), new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(883) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(884), new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(895) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(897), new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(897) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(988));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(930));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(951));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(953));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 8, 48, 43, 225, DateTimeKind.Local).AddTicks(792));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 8, 48, 43, 108, DateTimeKind.Local).AddTicks(5984), "$2a$11$Src.FXy1WR156gfP0qGu9.aGR3.nE30edv3cWfIV5QLnMJ1rLCBDa", new DateTime(2024, 2, 14, 8, 48, 43, 108, DateTimeKind.Local).AddTicks(5998) });

            migrationBuilder.AddForeignKey(
                name: "fk_special_discounts_users_modified_by_user_id",
                table: "special_discounts",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
