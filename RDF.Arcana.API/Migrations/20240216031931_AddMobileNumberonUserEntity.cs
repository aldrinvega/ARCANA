using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMobileNumberonUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_special_discounts_request_id",
                table: "special_discounts");

            migrationBuilder.AddColumn<string>(
                name: "mobile_number",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(403), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(403) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(406), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(407) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(408), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(408) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(409), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(423) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(425), new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(426) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(510));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(514));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(460));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(478));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 16, 11, 19, 31, 687, DateTimeKind.Local).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "mobile_number", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 16, 11, 19, 31, 569, DateTimeKind.Local).AddTicks(3981), null, "$2a$11$n8CUbyQBmW88foI3Iwzede52ptRAmepTnuCvb2eT32evkiC8o1G6e", new DateTime(2024, 2, 16, 11, 19, 31, 569, DateTimeKind.Local).AddTicks(3998) });

            migrationBuilder.CreateIndex(
                name: "ix_special_discounts_request_id",
                table: "special_discounts",
                column: "request_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_special_discounts_request_id",
                table: "special_discounts");

            migrationBuilder.DropColumn(
                name: "mobile_number",
                table: "users");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4262), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4263) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4266), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4266) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4268), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4268) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4270), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4281) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4282), new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4283) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4318));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 2, 14, 16, 16, 41, 527, DateTimeKind.Local).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 2, 14, 16, 16, 41, 407, DateTimeKind.Local).AddTicks(5657), "$2a$11$yBePLrnLxriKoDuGxSIkA.lQKBEKY6lbQHJJm0ZcQsqHgq1k/Exe6", new DateTime(2024, 2, 14, 16, 16, 41, 407, DateTimeKind.Local).AddTicks(5670) });

            migrationBuilder.CreateIndex(
                name: "ix_special_discounts_request_id",
                table: "special_discounts",
                column: "request_id");
        }
    }
}
