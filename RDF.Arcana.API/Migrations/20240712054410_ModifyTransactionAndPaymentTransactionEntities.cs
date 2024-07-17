using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTransactionAndPaymentTransactionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "charge_invoice",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "charge_invoice_date_received",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "withholding_date_received",
                table: "payment_transactions");
            
            migrationBuilder.DropColumn(
                name: "sales_invoice",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "sales_invoice_date_received",
                table: "transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "invoice_attach_date_received",
                table: "transactions",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<string>(
                name: "invoice_attach",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "withholding_no",
                table: "payment_transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3594), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3596) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3603), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3606) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3611), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3612) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3615), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3646) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3651), new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3891));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3734));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 13, 44, 6, 768, DateTimeKind.Local).AddTicks(3438));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 13, 44, 6, 457, DateTimeKind.Local).AddTicks(6440), "$2a$11$W35SiPL1SESB0VlDAxPfm.1IdC9TtN5TLS3SLfgDe.zelsGGffKyS", new DateTime(2024, 7, 12, 13, 44, 6, 457, DateTimeKind.Local).AddTicks(6515) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "withholding_no",
                table: "payment_transactions");

            migrationBuilder.RenameColumn(
                name: "invoice_attach_date_received",
                table: "transactions",
                newName: "sales_invoice_date_received");

            migrationBuilder.RenameColumn(
                name: "invoice_attach",
                table: "transactions",
                newName: "sales_invoice");

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
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7633), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7634) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7638), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7639) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7641), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7642) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7644), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7661) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7663), new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7664) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7769));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7776));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7733));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 12, 11, 34, 18, 149, DateTimeKind.Local).AddTicks(7544));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 12, 11, 34, 17, 873, DateTimeKind.Local).AddTicks(9517), "$2a$11$qrV9Pn/A3Js8PInBI7u5huvnLAf9zUYp3yQyM/kH.HhUxuNAD.ILO", new DateTime(2024, 7, 12, 11, 34, 17, 873, DateTimeKind.Local).AddTicks(9536) });
        }
    }
}
