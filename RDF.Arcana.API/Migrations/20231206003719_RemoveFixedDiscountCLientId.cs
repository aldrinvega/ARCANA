using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFixedDiscountCLientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_fixed_discounts_clients_clients_id",
                table: "fixed_discounts");

            migrationBuilder.DropIndex(
                name: "ix_fixed_discounts_client_id",
                table: "fixed_discounts");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "fixed_discounts");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(777), new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(778) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(785), new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(787) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(800), new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(928), new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(929) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(979), new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(979) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(1182));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(1190));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(1055));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(1073));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(1110));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 8, 37, 18, 857, DateTimeKind.Local).AddTicks(405));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 6, 8, 37, 18, 571, DateTimeKind.Local).AddTicks(6211), "$2a$11$Dm57Kox6CY0Z9G/aja3E1ONdsfu9.apFLdH4o604NQsu51ZiAqKZu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "fixed_discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1161), new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1162) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1169), new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1172), new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1172) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1174), new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1175) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1201), new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1202) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1691));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1326));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(1433));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 6, 7, 20, 27, 591, DateTimeKind.Local).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 6, 7, 20, 27, 363, DateTimeKind.Local).AddTicks(9426), "$2a$11$OdL5XApSeVZs6UO2R6IpCuNC1vW1jLneu6bnZc0pv.STfzFibIAz2" });

            migrationBuilder.CreateIndex(
                name: "ix_fixed_discounts_client_id",
                table: "fixed_discounts",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_fixed_discounts_clients_clients_id",
                table: "fixed_discounts",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
