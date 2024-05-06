using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClearedPaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cleared_payments_id",
                table: "payment_transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cleared_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_record_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cleared_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_cleared_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_cleared_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4179), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4184) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4190), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4199), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4226) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4233), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4237) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4242), new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4246) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4438));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 24, 9, 12, 17, 670, DateTimeKind.Local).AddTicks(4032));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 24, 9, 12, 16, 874, DateTimeKind.Local).AddTicks(8329), "$2a$11$ekGt0ZKq4VL8tn.em/l1F.M84aP4oVxG9H767/UoJnwtYiBnnPjVi", new DateTime(2024, 4, 24, 9, 12, 16, 874, DateTimeKind.Local).AddTicks(8354) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_cleared_payments_id",
                table: "payment_transactions",
                column: "cleared_payments_id");

            migrationBuilder.CreateIndex(
                name: "ix_cleared_payments_added_by",
                table: "cleared_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_cleared_payments_modified_by",
                table: "cleared_payments",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_transactions_cleared_payments_cleared_payments_id",
                table: "payment_transactions",
                column: "cleared_payments_id",
                principalTable: "cleared_payments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_transactions_cleared_payments_cleared_payments_id",
                table: "payment_transactions");

            migrationBuilder.DropTable(
                name: "cleared_payments");

            migrationBuilder.DropIndex(
                name: "ix_payment_transactions_cleared_payments_id",
                table: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "cleared_payments_id",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8173), new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8237) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8318), new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8320) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8323), new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8324) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8351), new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8577) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8580), new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8583) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8776));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8920));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(8937));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 54, 43, 971, DateTimeKind.Local).AddTicks(7077));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 54, 43, 640, DateTimeKind.Local).AddTicks(5001), "$2a$11$yNR2YQhcNwem9j25lzM93eKKz.dNYHaaV4djaUE.LeryQodWs2hHW", new DateTime(2024, 4, 18, 13, 54, 43, 640, DateTimeKind.Local).AddTicks(5035) });
        }
    }
}
