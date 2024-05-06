using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddModifiedByOnPaymentReocrdsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "payment_records",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_payment_records_modified_by",
                table: "payment_records",
                column: "modified_by");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_records_users_modified_by_user_id",
                table: "payment_records",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_records_users_modified_by_user_id",
                table: "payment_records");

            migrationBuilder.DropIndex(
                name: "ix_payment_records_modified_by",
                table: "payment_records");

            migrationBuilder.AlterColumn<string>(
                name: "modified_by",
                table: "payment_records",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9247), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9251) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9259), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9264), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9265) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9268), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9298), new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9552));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9428));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 4, 18, 13, 49, 18, 626, DateTimeKind.Local).AddTicks(8719));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 4, 18, 13, 49, 18, 284, DateTimeKind.Local).AddTicks(4057), "$2a$11$ZHOtLf9oSur9/FdVCX13HOcVnerTxqX9AmZDBFsgOZLjOHCkBC7HC", new DateTime(2024, 4, 18, 13, 49, 18, 284, DateTimeKind.Local).AddTicks(4073) });
        }
    }
}
