using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceModeItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_item_price_changes_items_item_id",
                table: "item_price_changes");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "item_price_changes",
                newName: "price_mode_item_id");

            migrationBuilder.RenameIndex(
                name: "ix_item_price_changes_item_id",
                table: "item_price_changes",
                newName: "ix_item_price_changes_price_mode_item_id");

            migrationBuilder.AddColumn<int>(
                name: "items_id",
                table: "item_price_changes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "price_mode_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    price_mode_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    i_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_price_mode_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_price_mode_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_price_mode_items_price_mode_price_mode_id",
                        column: x => x.price_mode_id,
                        principalTable: "price_mode",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_price_mode_items_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_price_mode_items_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_price_mode_items_added_by",
                table: "price_mode_items",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_price_mode_items_item_id",
                table: "price_mode_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_price_mode_items_modified_by",
                table: "price_mode_items",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_price_mode_items_price_mode_id",
                table: "price_mode_items",
                column: "price_mode_id");

            migrationBuilder.AddForeignKey(
                name: "fk_item_price_changes_items_items_id",
                table: "item_price_changes",
                column: "items_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_item_price_changes_price_mode_items_price_mode_item_id",
                table: "item_price_changes",
                column: "price_mode_item_id",
                principalTable: "price_mode_items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_item_price_changes_items_items_id",
                table: "item_price_changes");

            migrationBuilder.DropForeignKey(
                name: "fk_item_price_changes_price_mode_items_price_mode_item_id",
                table: "item_price_changes");

            migrationBuilder.DropTable(
                name: "price_mode_items");

            migrationBuilder.DropIndex(
                name: "ix_item_price_changes_items_id",
                table: "item_price_changes");

            migrationBuilder.DropColumn(
                name: "items_id",
                table: "item_price_changes");

            migrationBuilder.RenameColumn(
                name: "price_mode_item_id",
                table: "item_price_changes",
                newName: "item_id");

            migrationBuilder.RenameIndex(
                name: "ix_item_price_changes_price_mode_item_id",
                table: "item_price_changes",
                newName: "ix_item_price_changes_item_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2338), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2345), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2346) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2349), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2353), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2374) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2377), new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2378) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2441));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2480));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 2, 11, 0, 24, 738, DateTimeKind.Local).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 2, 11, 0, 24, 433, DateTimeKind.Local).AddTicks(5213), "$2a$11$z5E03fJGzvOwaH7qvon5teqtHvfDI3mgF5Nd2wW/WHrIsCOYE29cS", new DateTime(2024, 2, 2, 11, 0, 24, 433, DateTimeKind.Local).AddTicks(5240) });

            migrationBuilder.AddForeignKey(
                name: "fk_item_price_changes_items_item_id",
                table: "item_price_changes",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
