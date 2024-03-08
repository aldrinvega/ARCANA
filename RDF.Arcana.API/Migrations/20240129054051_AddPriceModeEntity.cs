using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceModeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "price_mode",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_price_mode", x => x.id);
                    table.ForeignKey(
                        name: "fk_price_mode_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_price_mode_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7236), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7237) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7240), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7240) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7242), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7257) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7259), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7259) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7261), new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7261) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7317));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 29, 13, 40, 50, 213, DateTimeKind.Local).AddTicks(7162));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 29, 13, 40, 49, 612, DateTimeKind.Local).AddTicks(6555), "$2a$11$fHxvm6AYyji3LDXuNNM9xOEuTFDbJD.DnBvjGJWHZ.ftV6l3skRDi", new DateTime(2024, 1, 29, 13, 40, 49, 612, DateTimeKind.Local).AddTicks(6572) });

            migrationBuilder.CreateIndex(
                name: "ix_price_mode_added_by",
                table: "price_mode",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_price_mode_modified_by",
                table: "price_mode",
                column: "modified_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "price_mode");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8158), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8161) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8182), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8184) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8189), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8193) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8198), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8265), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8267) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8609));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8480));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8485));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 48, 652, DateTimeKind.Local).AddTicks(9220), "$2a$11$P68HUY03ZMckLGP2BDTi0e5ZInsjehhjdfJTgl/je7XZvAxJ3ql/G", new DateTime(2024, 1, 22, 11, 36, 48, 652, DateTimeKind.Local).AddTicks(9299) });
        }
    }
}
