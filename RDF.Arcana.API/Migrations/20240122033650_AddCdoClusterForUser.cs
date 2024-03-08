using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCdoClusterForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clusters_users_user_id",
                table: "clusters");

            migrationBuilder.DropIndex(
                name: "ix_clusters_user_id",
                table: "clusters");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "clusters");

            migrationBuilder.CreateTable(
                name: "cdo_clusters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cluster_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    creates_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cdo_clusters", x => x.id);
                    table.ForeignKey(
                        name: "fk_cdo_clusters_clusters_cluster_id",
                        column: x => x.cluster_id,
                        principalTable: "clusters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cdo_clusters_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8158), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8161) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8182), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8184) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8189), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8193) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8198), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8265), new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8267) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8609));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8480));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(8485));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 22, 11, 36, 49, 160, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 22, 11, 36, 48, 652, DateTimeKind.Local).AddTicks(9220), "$2a$11$P68HUY03ZMckLGP2BDTi0e5ZInsjehhjdfJTgl/je7XZvAxJ3ql/G", new DateTime(2024, 1, 22, 11, 36, 48, 652, DateTimeKind.Local).AddTicks(9299) });

            migrationBuilder.CreateIndex(
                name: "ix_cdo_clusters_cluster_id",
                table: "cdo_clusters",
                column: "cluster_id");

            migrationBuilder.CreateIndex(
                name: "ix_cdo_clusters_user_id",
                table: "cdo_clusters",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cdo_clusters");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "clusters",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1379), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1382) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1391), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1392) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1395), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1426) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1431), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1432) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1435), new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1436) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1582));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(1535));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 15, 15, 49, 5, 227, DateTimeKind.Local).AddTicks(812));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 49, 4, 377, DateTimeKind.Local).AddTicks(699), "$2a$11$JwJf6En6qUJj/utgWLRgROSgEwIE.1w9E/7Db9LFg6acOtlgrcMvW", new DateTime(2024, 1, 15, 15, 49, 4, 377, DateTimeKind.Local).AddTicks(736) });

            migrationBuilder.CreateIndex(
                name: "ix_clusters_user_id",
                table: "clusters",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "fk_clusters_users_user_id",
                table: "clusters",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
