using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustClusterInClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "cluster",
                table: "clients",
                newName: "cluster_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1565), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1568) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1576), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1577) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1580), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1581) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1585), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1603) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1605), new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1605) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1723));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1647));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 21, 13, 34, 59, 946, DateTimeKind.Local).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 21, 13, 34, 59, 477, DateTimeKind.Local).AddTicks(3834), "$2a$11$z1iBsW7UostN8asGtFZaNe7gwH100lNUH3XSHEZAEP0jRUMYgWMty" });

            migrationBuilder.CreateIndex(
                name: "ix_clients_cluster_id",
                table: "clients",
                column: "cluster_id");

            migrationBuilder.CreateIndex(
                name: "ix_cdo_clusters_user_id",
                table: "cdo_clusters",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_clusters_cluster_id",
                table: "clients",
                column: "cluster_id",
                principalTable: "clusters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "ix_clients_cluster_id",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "ix_cdo_clusters_user_id",
                table: "cdo_clusters");

            migrationBuilder.RenameColumn(
                name: "cluster_id",
                table: "clients",
                newName: "cluster");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1270), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1271) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1277), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1279) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1281), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1282) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1283), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1309), new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1309) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1443));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1353));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1369));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2023, 12, 19, 14, 33, 8, 15, DateTimeKind.Local).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 33, 7, 420, DateTimeKind.Local).AddTicks(6215), "$2a$11$dOlcO7DCpuby88VvCNatN.YoubMaUQJaAcK9jQRATJtbnzyp7phm6" });

            migrationBuilder.CreateIndex(
                name: "ix_cdo_clusters_user_id",
                table: "cdo_clusters",
                column: "user_id",
                unique: true);
        }
    }
}
