using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustClusterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2447), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2448) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2451), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2452) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2453), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2454) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2456), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2523) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2524), new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2525) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2662));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2728));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 1, 11, 11, 31, 54, 184, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 1, 11, 11, 31, 53, 921, DateTimeKind.Local).AddTicks(2067), "$2a$11$pqm44LFK5YEYUEHc74kgSeP3o7VmiK68Hu/5gwWMZwGwTsB4dnpRy", new DateTime(2024, 1, 11, 11, 31, 53, 921, DateTimeKind.Local).AddTicks(2080) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "ix_cdo_clusters_cluster_id",
                table: "cdo_clusters",
                column: "cluster_id");

            migrationBuilder.CreateIndex(
                name: "ix_cdo_clusters_user_id",
                table: "cdo_clusters",
                column: "user_id");
        }
    }
}
