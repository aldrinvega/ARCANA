using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUploadSICIWithholding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "charge_invoice",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "charge_invoice_date_received",
                table: "transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "sales_invoice_date_received",
                table: "transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "withholding_date_received",
                table: "payment_transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9425), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9435), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9436) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9439), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9441) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9445), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9467) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9471), new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9473) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9620));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(9565));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 10, 12, 54, 24, 273, DateTimeKind.Local).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 10, 12, 54, 23, 535, DateTimeKind.Local).AddTicks(1899), "$2a$11$pu2GozJn7//laB/oyeoS2.cHlVkwIS85zx8Np0YDdmU.guz1IeY4O", new DateTime(2024, 6, 10, 12, 54, 23, 535, DateTimeKind.Local).AddTicks(1939) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "charge_invoice",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "charge_invoice_date_received",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "sales_invoice_date_received",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "withholding_date_received",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5082), new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5082) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5086), new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5087) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5089), new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5089) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5091), new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5129) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5131), new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5132) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5296));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 6, 6, 13, 11, 3, 286, DateTimeKind.Local).AddTicks(4866));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 6, 13, 11, 2, 685, DateTimeKind.Local).AddTicks(6678), "$2a$11$YbxxiaetZfclYh1/duAM1uOPSILGFKWwkTSs0M0oPB.FU72a4aei6", new DateTime(2024, 6, 6, 13, 11, 2, 685, DateTimeKind.Local).AddTicks(6720) });
        }
    }
}
