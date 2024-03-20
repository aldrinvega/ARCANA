using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations;

/// <inheritdoc />
public partial class AddMissingColumnForTransactionsEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "uopdated_at",
            table: "transaction_sales",
            newName: "updated_at");

        migrationBuilder.AddColumn<string>(
            name: "charge_invoice_no",
            table: "transaction_sales",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "discount",
            table: "transaction_sales",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "discount_amount",
            table: "transaction_sales",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "special_discount",
            table: "transaction_sales",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "special_discount_amount",
            table: "transaction_sales",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "sub_total",
            table: "transaction_sales",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 1,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(446), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(446) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 2,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(450), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(451) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 3,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(452), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(452) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 4,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(454), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(469) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 5,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(571), new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(572) });

        migrationBuilder.UpdateData(
            table: "mode_of_payments",
            keyColumn: "id",
            keyValue: 1,
            column: "created_at",
            value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(680));

        migrationBuilder.UpdateData(
            table: "mode_of_payments",
            keyColumn: "id",
            keyValue: 2,
            column: "created_at",
            value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(684));

        migrationBuilder.UpdateData(
            table: "terms",
            keyColumn: "id",
            keyValue: 1,
            column: "created_at",
            value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(614));

        migrationBuilder.UpdateData(
            table: "terms",
            keyColumn: "id",
            keyValue: 2,
            column: "created_at",
            value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(642));

        migrationBuilder.UpdateData(
            table: "terms",
            keyColumn: "id",
            keyValue: 3,
            column: "created_at",
            value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(644));

        migrationBuilder.UpdateData(
            table: "user_roles",
            keyColumn: "id",
            keyValue: 1,
            column: "created_at",
            value: new DateTime(2024, 3, 19, 11, 34, 46, 415, DateTimeKind.Local).AddTicks(355));

        migrationBuilder.UpdateData(
            table: "users",
            keyColumn: "id",
            keyValue: 1,
            columns: new[] { "created_at", "password", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 19, 11, 34, 46, 287, DateTimeKind.Local).AddTicks(7151), "$2a$11$qBA/EIzxvWahpfhA62fGO.CuJ.xsNuiBLC01SXO59a7QZrKOOJLMW", new DateTime(2024, 3, 19, 11, 34, 46, 287, DateTimeKind.Local).AddTicks(7165) });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "charge_invoice_no",
            table: "transaction_sales");

        migrationBuilder.DropColumn(
            name: "discount",
            table: "transaction_sales");

        migrationBuilder.DropColumn(
            name: "discount_amount",
            table: "transaction_sales");

        migrationBuilder.DropColumn(
            name: "special_discount",
            table: "transaction_sales");

        migrationBuilder.DropColumn(
            name: "special_discount_amount",
            table: "transaction_sales");

        migrationBuilder.DropColumn(
            name: "sub_total",
            table: "transaction_sales");

        migrationBuilder.RenameColumn(
            name: "updated_at",
            table: "transaction_sales",
            newName: "uopdated_at");

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 1,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8087), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8088) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 2,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8092), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8093) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 3,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8094), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8094) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 4,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8097), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8122) });

        migrationBuilder.UpdateData(
            table: "booking_coverages",
            keyColumn: "id",
            keyValue: 5,
            columns: new[] { "created_at", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8124), new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8124) });

        migrationBuilder.UpdateData(
            table: "mode_of_payments",
            keyColumn: "id",
            keyValue: 1,
            column: "created_at",
            value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8233));

        migrationBuilder.UpdateData(
            table: "mode_of_payments",
            keyColumn: "id",
            keyValue: 2,
            column: "created_at",
            value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8237));

        migrationBuilder.UpdateData(
            table: "terms",
            keyColumn: "id",
            keyValue: 1,
            column: "created_at",
            value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8166));

        migrationBuilder.UpdateData(
            table: "terms",
            keyColumn: "id",
            keyValue: 2,
            column: "created_at",
            value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8197));

        migrationBuilder.UpdateData(
            table: "terms",
            keyColumn: "id",
            keyValue: 3,
            column: "created_at",
            value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(8198));

        migrationBuilder.UpdateData(
            table: "user_roles",
            keyColumn: "id",
            keyValue: 1,
            column: "created_at",
            value: new DateTime(2024, 3, 14, 10, 42, 51, 190, DateTimeKind.Local).AddTicks(7992));

        migrationBuilder.UpdateData(
            table: "users",
            keyColumn: "id",
            keyValue: 1,
            columns: new[] { "created_at", "password", "updated_at" },
            values: new object[] { new DateTime(2024, 3, 14, 10, 42, 51, 67, DateTimeKind.Local).AddTicks(3040), "$2a$11$ks9CytQnHPy/NIy648xa5OsuzTb1Jcx1j7mZkR9Zn4o1C3RZxVZ7.", new DateTime(2024, 3, 14, 10, 42, 51, 67, DateTimeKind.Local).AddTicks(3054) });
    }
}
