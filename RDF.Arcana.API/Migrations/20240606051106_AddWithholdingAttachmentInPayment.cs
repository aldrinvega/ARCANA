using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddWithholdingAttachmentInPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "withholding_attachment",
                table: "payment_transactions",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "withholding_attachment",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4453), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4455) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4466), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4467) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4470), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4471) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4473), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4531) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4537), new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4538) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4923));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4934));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4670));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 31, 13, 24, 50, 663, DateTimeKind.Local).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 31, 13, 24, 50, 341, DateTimeKind.Local).AddTicks(2003), "$2a$11$X4WDIDTEIfY8Q62KTrwaoePq3Hb3IIKKbpXKZ7VaVMoUYFQbD8K3.", new DateTime(2024, 5, 31, 13, 24, 50, 341, DateTimeKind.Local).AddTicks(2072) });
        }
    }
}
