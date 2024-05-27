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
                    client_id = table.Column<int>(type: "int", nullable: false),
                    online_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_online_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_online_payments_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_online_payments_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_online_payments_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5110), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5115) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5127), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5129) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5138), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5141) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5148), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5202) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5210), new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5213) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5485));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5499));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5327));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 27, 10, 51, 57, 857, DateTimeKind.Local).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 51, 57, 29, DateTimeKind.Local).AddTicks(3898), "$2a$11$7xNqUX/f1JG0iGO4Gk7yweGtTEZDW27qJFTM2A.IrH2SsBPwNsn0S", new DateTime(2024, 5, 27, 10, 51, 57, 29, DateTimeKind.Local).AddTicks(4022) });

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_added_by",
                table: "online_payments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_client_id",
                table: "online_payments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_modified_by",
                table: "online_payments",
                column: "modified_by");
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
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3616), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3621) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3634), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3635) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3638), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3667) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3673), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3680), new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3681) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3892));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3902));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 5, 22, 8, 19, 55, 6, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 19, 54, 378, DateTimeKind.Local).AddTicks(992), "$2a$11$.7Xk0PrCgzUSEy2W88VRSe7jl8epmeDCdzCh7E5m/NB14H7j9LKka", new DateTime(2024, 5, 22, 8, 19, 54, 378, DateTimeKind.Local).AddTicks(1082) });
        }
    }
}
