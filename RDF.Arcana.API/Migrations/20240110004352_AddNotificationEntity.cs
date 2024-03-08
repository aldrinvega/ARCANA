using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_read = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1401), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1404) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1411), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1412) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1415), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1441) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1445), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1446) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1449), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1451) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1593));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1509));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1541));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 51, 620, DateTimeKind.Local).AddTicks(7381), "$2a$11$MAUbxKRdGU9HgzOwznspQe3n/KjMxfzGwMQJzKWzfM91YMLptodEq", new DateTime(2024, 1, 10, 8, 43, 51, 620, DateTimeKind.Local).AddTicks(7414) });

            migrationBuilder.CreateIndex(
                name: "ix_request_approvers_approver_id",
                table: "request_approvers",
                column: "approver_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_request_approvers_users_approver_id",
                table: "request_approvers",
                column: "approver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_request_approvers_users_approver_id",
                table: "request_approvers");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropIndex(
                name: "ix_request_approvers_approver_id",
                table: "request_approvers");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1767), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1769) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1777), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1779) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1783), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1804) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1809), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1814), new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1982));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1928));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 3, 16, 16, 29, 100, DateTimeKind.Local).AddTicks(1627));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 3, 16, 16, 28, 245, DateTimeKind.Local).AddTicks(1430), "$2a$11$ljJd0uxaJmJ6m..0fxUNN..QMQb6/2b9.Hcp1U4Oi3ZSKRcmu4/O.", new DateTime(2024, 1, 3, 16, 16, 28, 245, DateTimeKind.Local).AddTicks(1465) });
        }
    }
}
