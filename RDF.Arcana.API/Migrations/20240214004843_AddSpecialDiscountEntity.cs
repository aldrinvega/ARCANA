using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialDiscountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "special_discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    request_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_one_time = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_special_discounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_special_discounts_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_special_discounts_requests_request_id",
                        column: x => x.request_id,
                        principalTable: "requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_special_discounts_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_special_discounts_users_modified_by_user_id",
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

            migrationBuilder.CreateIndex(
                name: "ix_special_discounts_added_by",
                table: "special_discounts",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_special_discounts_client_id",
                table: "special_discounts",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_special_discounts_modified_by",
                table: "special_discounts",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_special_discounts_request_id",
                table: "special_discounts",
                column: "request_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "special_discounts");

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
        }
    }
}
