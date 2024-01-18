using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClusterIdToTheNotifricationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cluster_id",
                table: "notifications",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2869), new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2872) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2881), new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2883) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2888), new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2898), new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2951) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2960), new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2963) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(3493));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(3504));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(3233));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(3244));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 10, 56, 24, 154, DateTimeKind.Local).AddTicks(2626));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 10, 56, 23, 169, DateTimeKind.Local).AddTicks(4279), "$2a$11$NqRzf4MIKNpiTqSeJiz8f.Nv9e/VerYVuVanJwLJGDEOJv1qe1eze", new DateTime(2024, 1, 10, 10, 56, 23, 169, DateTimeKind.Local).AddTicks(4379) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cluster_id",
                table: "notifications");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1401), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1404) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1411), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1412) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1415), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1441) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1445), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1446) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1449), new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1451) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1593));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1509));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1541));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 10, 8, 43, 52, 150, DateTimeKind.Local).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 43, 51, 620, DateTimeKind.Local).AddTicks(7381), "$2a$11$MAUbxKRdGU9HgzOwznspQe3n/KjMxfzGwMQJzKWzfM91YMLptodEq", new DateTime(2024, 1, 10, 8, 43, 51, 620, DateTimeKind.Local).AddTicks(7414) });
        }
    }
}
