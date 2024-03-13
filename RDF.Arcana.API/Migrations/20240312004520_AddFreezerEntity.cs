using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFreezerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "freezer",
                table: "clients");

            migrationBuilder.AddColumn<int>(
                name: "freezer_id",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "freezers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    assete_tag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freezers", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2526), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2527) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2530), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2531) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2533), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2534) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2535), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2548) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2549), new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2659));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2664));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2599));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2622));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2624));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 12, 8, 45, 19, 809, DateTimeKind.Local).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 12, 8, 45, 19, 674, DateTimeKind.Local).AddTicks(6254), "$2a$11$D9WL0CNwHBMrZdLEdvzaderqb8BoqKbr4kiTCa51R5UJXe0wjyX66", new DateTime(2024, 3, 12, 8, 45, 19, 674, DateTimeKind.Local).AddTicks(6269) });

            migrationBuilder.CreateIndex(
                name: "ix_clients_freezer_id",
                table: "clients",
                column: "freezer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_freezers_freezer_id",
                table: "clients",
                column: "freezer_id",
                principalTable: "freezers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_freezers_freezer_id",
                table: "clients");

            migrationBuilder.DropTable(
                name: "freezers");

            migrationBuilder.DropIndex(
                name: "ix_clients_freezer_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "freezer_id",
                table: "clients");

            migrationBuilder.AddColumn<bool>(
                name: "freezer",
                table: "clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6164), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6168), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6168) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6170), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6171), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6188) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6189), new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6291));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6295));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6255));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6256));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 11, 9, 39, 59, 167, DateTimeKind.Local).AddTicks(6043));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 11, 9, 39, 59, 43, DateTimeKind.Local).AddTicks(582), "$2a$11$i0dXEkVqJTFF4Ck8PsKoceqd066tSwZmD60LrNIPU8i2585XqVfhK", new DateTime(2024, 3, 11, 9, 39, 59, 43, DateTimeKind.Local).AddTicks(594) });
        }
    }
}
