using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustAdvancePaymentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cash_advance_payments_users_modified_by_user_id",
                table: "cash_advance_payments");

            migrationBuilder.DropForeignKey(
                name: "fk_cheque_advance_payments_users_modified_by_user_id",
                table: "cheque_advance_payments");

            migrationBuilder.DropForeignKey(
                name: "fk_online_advance_payments_users_modifed_by_user_id",
                table: "online_advance_payments");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "online_advance_payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "cheque_advance_payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "cash_advance_payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3887), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3888) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3892), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3893) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3895), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3896) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3897), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3916) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3917), new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3918) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(4031));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3962));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3992));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3994));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 16, 56, 29, 416, DateTimeKind.Local).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 16, 56, 29, 206, DateTimeKind.Local).AddTicks(4879), "$2a$11$2RA9dPqod8V.gOL0BN27i.gmqYtxJMcd8FdHsxfAfkvTuMp3udYdm", new DateTime(2024, 3, 20, 16, 56, 29, 206, DateTimeKind.Local).AddTicks(4897) });

            migrationBuilder.AddForeignKey(
                name: "fk_cash_advance_payments_users_modified_by_user_id",
                table: "cash_advance_payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cheque_advance_payments_users_modified_by_user_id",
                table: "cheque_advance_payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_online_advance_payments_users_modifed_by_user_id",
                table: "online_advance_payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cash_advance_payments_users_modified_by_user_id",
                table: "cash_advance_payments");

            migrationBuilder.DropForeignKey(
                name: "fk_cheque_advance_payments_users_modified_by_user_id",
                table: "cheque_advance_payments");

            migrationBuilder.DropForeignKey(
                name: "fk_online_advance_payments_users_modifed_by_user_id",
                table: "online_advance_payments");

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "online_advance_payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "cheque_advance_payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "modified_by",
                table: "cash_advance_payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9914), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9915) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9918), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9918) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9920), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9921) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9922), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9935) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9936), new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9936) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 233, DateTimeKind.Local).AddTicks(117));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 233, DateTimeKind.Local).AddTicks(120));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9992));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9994));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 3, 20, 14, 52, 16, 232, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 20, 14, 52, 16, 114, DateTimeKind.Local).AddTicks(4746), "$2a$11$CqJuH3MEaWPHsPHNiOY17uM7u1LX7ldA3P9RPeL6cuKvKDiqDioGK", new DateTime(2024, 3, 20, 14, 52, 16, 114, DateTimeKind.Local).AddTicks(4759) });

            migrationBuilder.AddForeignKey(
                name: "fk_cash_advance_payments_users_modified_by_user_id",
                table: "cash_advance_payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cheque_advance_payments_users_modified_by_user_id",
                table: "cheque_advance_payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_online_advance_payments_users_modifed_by_user_id",
                table: "online_advance_payments",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
