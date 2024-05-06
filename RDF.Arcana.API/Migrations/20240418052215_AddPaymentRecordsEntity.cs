using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentRecordsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "origin",
                table: "advance_payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payment_records",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_records", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_records_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(920), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(927) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(943), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(946) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(954), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(998) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1005), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1008) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1014), new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1017) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1300));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1314));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1202));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 22, 12, 702, DateTimeKind.Local).AddTicks(658));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 22, 11, 331, DateTimeKind.Local).AddTicks(617), "$2a$11$UzVPXvYY7EGV4jBe5DJIHu1aYU9wFaI7QYc.iZLjBGYFcQZWvEsHu", new DateTime(2024, 4, 18, 13, 22, 11, 331, DateTimeKind.Local).AddTicks(637) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_records_added_by",
                table: "payment_records",
                column: "added_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_records");

            migrationBuilder.DropColumn(
                name: "origin",
                table: "advance_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8226), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8227) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8231), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8232) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8233), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8234) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8235), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8254) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8256), new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8256) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8476));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 1, 9, 3, 7, 496, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 1, 9, 3, 7, 356, DateTimeKind.Local).AddTicks(1075), "$2a$11$gePchTD6WfWy4YQo27GHQugeqA757kT8.vO/y56MGJkLjxmiKuuuC", new DateTime(2024, 4, 1, 9, 3, 7, 356, DateTimeKind.Local).AddTicks(1090) });
        }
    }
}
