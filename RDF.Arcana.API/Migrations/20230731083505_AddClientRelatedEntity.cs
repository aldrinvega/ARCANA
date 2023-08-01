using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientRelatedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registration_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_registration_status", x => x.id);
                    table.ForeignKey(
                        name: "fk_registration_status_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_registration_status_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    owners_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    owners_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    registration_status_id = table.Column<int>(type: "int", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    email_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    freezer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    customer_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direct_delivery = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    booking_coverage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payment_method = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    term_days_id = table.Column<int>(type: "int", nullable: true),
                    discount_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clients_discounts_discount_id",
                        column: x => x.discount_id,
                        principalTable: "discounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clients_registration_status_registration_status_id",
                        column: x => x.registration_status_id,
                        principalTable: "registration_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clients_term_days_term_days_id",
                        column: x => x.term_days_id,
                        principalTable: "term_days",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clients_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clients_users_approved_by_user_id",
                        column: x => x.approved_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clients_users_modified_by_user_id",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clients_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "permit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    business_permit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permit", x => x.id);
                    table.ForeignKey(
                        name: "fk_permit_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permit_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clients_added_by",
                table: "clients",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_approved_by",
                table: "clients",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_department_id",
                table: "clients",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_discount_id",
                table: "clients",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_modified_by",
                table: "clients",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_registration_status_id",
                table: "clients",
                column: "registration_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_term_days_id",
                table: "clients",
                column: "term_days_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_user_id",
                table: "clients",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_permit_client_id",
                table: "permit",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_permit_user_id",
                table: "permit",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_registration_status_added_by",
                table: "registration_status",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_registration_status_modified_by",
                table: "registration_status",
                column: "modified_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permit");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "registration_status");
        }
    }
}
