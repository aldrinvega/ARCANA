using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNextApproverIdonRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "next_approver_id",
                table: "requests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8427), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8428) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8432), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8432) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8436), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8437) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8438), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8455) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8456), new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8456) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8551));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8555));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8518));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8520));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 21, 11, 24, 35, 954, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 21, 11, 24, 35, 834, DateTimeKind.Local).AddTicks(5130), "$2a$11$Io4mICB/KRwTiI3mCRcZIOUBZMBLSVz3AawptrQF8Xdyr4SKmQ1Qa", new DateTime(2024, 2, 21, 11, 24, 35, 834, DateTimeKind.Local).AddTicks(5144) });

            migrationBuilder.CreateIndex(
                name: "ix_requests_next_approver_id",
                table: "requests",
                column: "next_approver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_requests_users_next_approver_id",
                table: "requests",
                column: "next_approver_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_requests_users_next_approver_id",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "ix_requests_next_approver_id",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "next_approver_id",
                table: "requests");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7101), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7101) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7104), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7106), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7106) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7107), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7127), new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7128) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7181));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 19, 14, 0, 56, 202, DateTimeKind.Local).AddTicks(7011));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 19, 14, 0, 56, 83, DateTimeKind.Local).AddTicks(281), "$2a$11$WHyNAlYWdi4rYE4xnsmTBeLu1aNWPxBsPN.NLtVuoijZEL6p8No3W", new DateTime(2024, 2, 19, 14, 0, 56, 83, DateTimeKind.Local).AddTicks(295) });
        }
    }
}
