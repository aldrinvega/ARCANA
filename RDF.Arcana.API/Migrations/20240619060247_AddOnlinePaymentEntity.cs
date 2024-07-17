using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOnlinePaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "online_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    online_platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_online_payments", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9167), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9168) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9175), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9188), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9192) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9199), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9263) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9273), new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9275) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 14, 2, 43, 653, DateTimeKind.Local).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 14, 2, 43, 191, DateTimeKind.Local).AddTicks(5274), "$2a$11$nkXiBvpZDZquBwNUjP4pr.aBSpjpJjKcdRrkzQF2nH8RN8EbbDe2m", new DateTime(2024, 6, 19, 14, 2, 43, 191, DateTimeKind.Local).AddTicks(5357) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "online_payments");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3205), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3206) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3223), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3223) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3225), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3281), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3282) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3283), new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3284) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3942));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3452));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(3795));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 19, 13, 0, 40, 192, DateTimeKind.Local).AddTicks(2575));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 19, 13, 0, 39, 862, DateTimeKind.Local).AddTicks(7855), "$2a$11$FEh9awJSqOOSODatp7wNPOLX5p3dFxVdGLZ9/GPMB27jWcD4XI40.", new DateTime(2024, 6, 19, 13, 0, 39, 862, DateTimeKind.Local).AddTicks(7880) });
        }
    }
}
