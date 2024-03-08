using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceModeIdonClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price_mode_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(456), new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(458) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(464), new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(465) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(467), new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(468) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(471), new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(495) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(497), new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(622));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(627));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(539));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(573));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 5, 10, 41, 33, 82, DateTimeKind.Local).AddTicks(80));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 5, 10, 41, 32, 723, DateTimeKind.Local).AddTicks(5614), "$2a$11$gwhRvdFvcXGwc82epmv/iOzG.sx3d.6jPwdHnKyHandBbQhzh7V4.", new DateTime(2024, 2, 5, 10, 41, 32, 723, DateTimeKind.Local).AddTicks(5661) });

            migrationBuilder.CreateIndex(
                name: "ix_clients_price_mode_id",
                table: "clients",
                column: "price_mode_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_price_mode_price_mode_id",
                table: "clients",
                column: "price_mode_id",
                principalTable: "price_mode",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_price_mode_price_mode_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_clients_price_mode_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "price_mode_id",
                table: "clients");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8799), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8802) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8806), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8807) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8809), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8812), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8834) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8840), new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8843) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(9049));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8916));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8941));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8946));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 3, 21, 24, 42, 340, DateTimeKind.Local).AddTicks(8670));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 3, 21, 24, 42, 33, DateTimeKind.Local).AddTicks(4376), "$2a$11$KXUJUS2FwgmNUtqe/QIbouyP280herHIrgt3YO0FCQUy2b.p9Wv3u", new DateTime(2024, 2, 3, 21, 24, 42, 33, DateTimeKind.Local).AddTicks(4401) });
        }
    }
}
