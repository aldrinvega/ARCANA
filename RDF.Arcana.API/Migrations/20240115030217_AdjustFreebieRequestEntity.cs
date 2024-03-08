using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustFreebieRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_approvals_approvals_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_users_user_id",
                table: "notifications");

            migrationBuilder.DropTable(
                name: "approvals");

            migrationBuilder.DropIndex(
                name: "ix_freebie_requests_approvals_id",
                table: "freebie_requests");

            migrationBuilder.DropColumn(
                name: "approvals_id",
                table: "freebie_requests");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9330), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9332) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9336), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9340), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9342), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9360) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9362), new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9363) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9491));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9496));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9449));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 11, 2, 16, 49, DateTimeKind.Local).AddTicks(9183));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 11, 2, 15, 691, DateTimeKind.Local).AddTicks(5898), "$2a$11$R/1TdTyZkzyMFPcAyPA5JutI/N3PIssZdP1rxsIIX2X0rJ9bIeeqa", new DateTime(2024, 1, 15, 11, 2, 15, 691, DateTimeKind.Local).AddTicks(5936) });

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_users_user_id",
                table: "notifications",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notifications_users_user_id",
                table: "notifications");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "approvals_id",
                table: "freebie_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "approvals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    requested_by = table.Column<int>(type: "int", nullable: false),
                    approval_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approvals", x => x.id);
                    table.ForeignKey(
                        name: "fk_approvals_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approvals_users_approve_by_user_id",
                        column: x => x.approved_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_approvals_users_requested_by_user_id",
                        column: x => x.requested_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9694), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9695) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9700), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9701) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9703), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9703) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9705), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9722) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9724), new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9725) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9846));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 14, 33, 26, 686, DateTimeKind.Local).AddTicks(9548));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 33, 26, 379, DateTimeKind.Local).AddTicks(8281), "$2a$11$UN.Dx8IjoR6hmNVK68Mv5OyoUnS/RklN7hBAUDClttV6yK/lkfoHi", new DateTime(2024, 1, 11, 14, 33, 26, 379, DateTimeKind.Local).AddTicks(8359) });

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approvals_id",
                table: "freebie_requests",
                column: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_approved_by",
                table: "approvals",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_client_id",
                table: "approvals",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_requested_by",
                table: "approvals",
                column: "requested_by");

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_approvals_approvals_id",
                table: "freebie_requests",
                column: "approvals_id",
                principalTable: "approvals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_users_user_id",
                table: "notifications",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
