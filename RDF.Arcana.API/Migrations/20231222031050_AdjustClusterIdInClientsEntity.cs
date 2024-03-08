using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustClusterIdInClientsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_clusters_cluster_id",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "cluster_id",
                table: "clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4301), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4301) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4305), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4306) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4308), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4309) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4311), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4311) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4326), new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4326) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4367));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4387));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 11, 10, 49, 379, DateTimeKind.Local).AddTicks(4205));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 22, 11, 10, 49, 43, DateTimeKind.Local).AddTicks(3603), "$2a$11$DS9wSDjAI3/jOrx53EsCEO3ldStWdLww5f8RfHqLf8g8hez3QVC02" });

            migrationBuilder.AddForeignKey(
                name: "fk_clients_clusters_cluster_id",
                table: "clients",
                column: "cluster_id",
                principalTable: "clusters",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clients_clusters_cluster_id",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "cluster_id",
                table: "clients",
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
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9525), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9534), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9535) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9537), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9538) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9539), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9562) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9564), new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9565) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9683));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9624));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9652));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 22, 8, 20, 5, 172, DateTimeKind.Local).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 20, 4, 606, DateTimeKind.Local).AddTicks(879), "$2a$11$Em5gsex4vTjjRVY61YgUSeAqYNp0TT/iJdZiuC2XPIQBsdJjcolgG" });

            migrationBuilder.AddForeignKey(
                name: "fk_clients_clusters_cluster_id",
                table: "clients",
                column: "cluster_id",
                principalTable: "clusters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
