using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "fixed_discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    discount_percentage = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fixed_discounts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "variable_discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    minimum_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    maximum_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    minimum_percentage = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    maximum_percentage = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    is_subject_to_approval = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_variable_discounts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "approvals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    approval_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_approved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approvals", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "approved_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_approved = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approved_clients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "approved_freebies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    freebies_id = table.Column<int>(type: "int", nullable: false),
                    transaction_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: false),
                    photo_proof_path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approved_freebies", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    owners_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    owners_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_of_birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    email_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("pk_client", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "client_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    document_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    document_path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_documents", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fullname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    representative_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    representative_position = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    freezer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    customer_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    term_days = table.Column<int>(type: "int", nullable: true),
                    discount_id = table.Column<int>(type: "int", nullable: true),
                    client_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    store_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    registration_status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fixed_discount_id = table.Column<int>(type: "int", nullable: true),
                    variable_discount_id = table.Column<int>(type: "int", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    fixed_discounts_id = table.Column<int>(type: "int", nullable: true),
                    variable_discounts_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_fixed_discounts_fixed_discounts_id",
                        column: x => x.fixed_discounts_id,
                        principalTable: "fixed_discounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clients_variable_discounts_variable_discounts_id",
                        column: x => x.variable_discounts_id,
                        principalTable: "variable_discounts",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "freebie_request",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    approval_id = table.Column<int>(type: "int", nullable: false),
                    approvals_id = table.Column<int>(type: "int", nullable: true),
                    is_approved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    clients_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_request", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_request_approvals_approvals_id",
                        column: x => x.approvals_id,
                        principalTable: "approvals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_freebie_request_clients_clients_id",
                        column: x => x.clients_id,
                        principalTable: "clients",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    department_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    lower_bound = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    upper_bound = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    commission_rate_lower = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    commission_rate_upper = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discounts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "freebie_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    transaction_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    approved_client_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_requests_approved_clients_approved_client_id",
                        column: x => x.approved_client_id,
                        principalTable: "approved_clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_freebie_requests_approved_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "approved_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "freebies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    approved_freebies_id = table.Column<int>(type: "int", nullable: true),
                    freebie_request_id = table.Column<int>(type: "int", nullable: true),
                    items_id = table.Column<int>(type: "int", nullable: true),
                    rejected_freebies_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebies", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebies_approved_clients_approved_client_id",
                        column: x => x.client_id,
                        principalTable: "approved_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_freebies_approved_freebies_approved_freebies_id",
                        column: x => x.approved_freebies_id,
                        principalTable: "approved_freebies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_freebies_freebie_requests_freebie_request_id",
                        column: x => x.freebie_request_id,
                        principalTable: "freebie_requests",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    item_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    item_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    product_sub_category_id = table.Column<int>(type: "int", nullable: false),
                    meat_type_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_items", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    location_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meat_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    meat_type_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meat_types", x => x.id);
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
                        name: "fk_permit_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_category_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_categories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_sub_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_sub_category_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_category_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sub_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_sub_categories_product_categories_product_category_id",
                        column: x => x.product_category_id,
                        principalTable: "product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rejected_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_rejected = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    rejected_by = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejected_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_rejected_clients_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rejected_freebies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    freebies_id = table.Column<int>(type: "int", nullable: false),
                    transaction_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejected_freebies", x => x.id);
                    table.ForeignKey(
                        name: "fk_rejected_freebies_freebies_freebies_id",
                        column: x => x.freebies_id,
                        principalTable: "freebies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "requested_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_request = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    requested_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requested_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_requested_clients_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    freebies_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status", x => x.id);
                    table.ForeignKey(
                        name: "fk_status_freebies_freebies_id",
                        column: x => x.freebies_id,
                        principalTable: "freebies",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_days",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    days = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_term_days", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "uoms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    uom_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uom_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uoms", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_role_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permissions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fullname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    location_id = table.Column<int>(type: "int", nullable: true),
                    user_role_id = table.Column<int>(type: "int", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    freebies_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_freebies_freebies_id",
                        column: x => x.freebies_id,
                        principalTable: "freebies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_user_roles_user_role_id",
                        column: x => x.user_role_id,
                        principalTable: "user_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_users_added_by",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_client_id",
                table: "approvals",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_approved_clients_approved_by",
                table: "approved_clients",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approved_clients_client_id",
                table: "approved_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_approved_clients_status",
                table: "approved_clients",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_approved_freebies_approved_by",
                table: "approved_freebies",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approved_freebies_freebies_id",
                table: "approved_freebies",
                column: "freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_approved_freebies_status_id",
                table: "approved_freebies",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_added_by",
                table: "client",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_client_department_id",
                table: "client",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_discount_id",
                table: "client",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_modified_by",
                table: "client",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_client_term_days_id",
                table: "client",
                column: "term_days_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_user_id",
                table: "client",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_documents_client_id",
                table: "client_documents",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_added_by",
                table: "clients",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_fixed_discounts_id",
                table: "clients",
                column: "fixed_discounts_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_modified_by",
                table: "clients",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_variable_discounts_id",
                table: "clients",
                column: "variable_discounts_id");

            migrationBuilder.CreateIndex(
                name: "ix_companies_added_by",
                table: "companies",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_departments_added_by",
                table: "departments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_discounts_added_by",
                table: "discounts",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_request_approvals_id",
                table: "freebie_request",
                column: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_request_clients_id",
                table: "freebie_request",
                column: "clients_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_added_by",
                table: "freebie_requests",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approved_client_id",
                table: "freebie_requests",
                column: "approved_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_client_id",
                table: "freebie_requests",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_status_id",
                table: "freebie_requests",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_approved_freebies_id",
                table: "freebies",
                column: "approved_freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_client_id",
                table: "freebies",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_freebie_request_id",
                table: "freebies",
                column: "freebie_request_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_item_id",
                table: "freebies",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_items_id",
                table: "freebies",
                column: "items_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebies_rejected_freebies_id",
                table: "freebies",
                column: "rejected_freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_added_by",
                table: "items",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_items_meat_type_id",
                table: "items",
                column: "meat_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_product_sub_category_id",
                table: "items",
                column: "product_sub_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_uom_id",
                table: "items",
                column: "uom_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_added_by",
                table: "locations",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_meat_types_added_by",
                table: "meat_types",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_permit_client_id",
                table: "permit",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_permit_user_id",
                table: "permit",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_categories_added_by",
                table: "product_categories",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_product_sub_categories_added_by",
                table: "product_sub_categories",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_product_sub_categories_product_category_id",
                table: "product_sub_categories",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_client_id",
                table: "rejected_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_rejected_by",
                table: "rejected_clients",
                column: "rejected_by");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_clients_status",
                table: "rejected_clients",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_added_by",
                table: "rejected_freebies",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_freebies_id",
                table: "rejected_freebies",
                column: "freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_freebies_status_id",
                table: "rejected_freebies",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_client_id",
                table: "requested_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_requested_by",
                table: "requested_clients",
                column: "requested_by");

            migrationBuilder.CreateIndex(
                name: "ix_requested_clients_status",
                table: "requested_clients",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_status_added_by",
                table: "status",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_status_freebies_id",
                table: "status",
                column: "freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_status_modified_by",
                table: "status",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_term_days_added_by",
                table: "term_days",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_uoms_added_by",
                table: "uoms",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_added_by",
                table: "user_roles",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_users_added_by",
                table: "users",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_users_company_id",
                table: "users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_department_id",
                table: "users",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_freebies_id",
                table: "users",
                column: "freebies_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_location_id",
                table: "users",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_role_id",
                table: "users",
                column: "user_role_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_approvals_clients_client_id",
                table: "approvals",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_clients_client_client_id",
                table: "approved_clients",
                column: "client_id",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_clients_status_approved_status_id",
                table: "approved_clients",
                column: "status",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_clients_users_approved_by_user_id",
                table: "approved_clients",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_freebies_freebies_freebies_id",
                table: "approved_freebies",
                column: "freebies_id",
                principalTable: "freebies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_freebies_status_status_id",
                table: "approved_freebies",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_approved_freebies_users_approved_by",
                table: "approved_freebies",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_client_departments_department_id",
                table: "client",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_client_discounts_discount_id",
                table: "client",
                column: "discount_id",
                principalTable: "discounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_client_term_days_term_days_id",
                table: "client",
                column: "term_days_id",
                principalTable: "term_days",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_client_users_added_by_user_id",
                table: "client",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_client_users_modified_by_user_id",
                table: "client",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_client_users_user_id",
                table: "client",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_client_documents_clients_clients_id",
                table: "client_documents",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_modified_by_user_id",
                table: "clients",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_requested_by_user_id",
                table: "clients",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_companies_users_added_by",
                table: "companies",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_users_added_by",
                table: "departments",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_discounts_users_added_by",
                table: "discounts",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_status_status_id",
                table: "freebie_requests",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_users_added_by",
                table: "freebie_requests",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_items_item_id",
                table: "freebies",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_items_items_id",
                table: "freebies",
                column: "items_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_freebies_rejected_freebies_rejected_freebies_id",
                table: "freebies",
                column: "rejected_freebies_id",
                principalTable: "rejected_freebies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_items_meat_types_meat_type_id",
                table: "items",
                column: "meat_type_id",
                principalTable: "meat_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_items_product_sub_categories_product_sub_category_id",
                table: "items",
                column: "product_sub_category_id",
                principalTable: "product_sub_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_items_uoms_uom_id",
                table: "items",
                column: "uom_id",
                principalTable: "uoms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_items_users_added_by",
                table: "items",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_users_added_by",
                table: "locations",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_meat_types_users_added_by",
                table: "meat_types",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_permit_users_user_id",
                table: "permit",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_categories_users_added_by",
                table: "product_categories",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_sub_categories_users_added_by",
                table: "product_sub_categories",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_clients_status_rejected_status_id",
                table: "rejected_clients",
                column: "status",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_clients_users_rejected_by_user_id",
                table: "rejected_clients",
                column: "rejected_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_status_status_id",
                table: "rejected_freebies",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rejected_freebies_users_added_by",
                table: "rejected_freebies",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_requested_clients_status_request_status_id",
                table: "requested_clients",
                column: "status",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_requested_clients_users_requested_by_user_id",
                table: "requested_clients",
                column: "requested_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_status_users_added_by_user_id",
                table: "status",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_status_users_modified_by_user_id",
                table: "status",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_term_days_users_added_by",
                table: "term_days",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_uoms_users_added_by",
                table: "uoms",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_roles_users_added_by",
                table: "user_roles",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_approved_clients_client_client_id",
                table: "approved_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_clients_status_approved_status_id",
                table: "approved_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_freebies_status_status_id",
                table: "approved_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_status_status_id",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_status_status_id",
                table: "rejected_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_clients_users_approved_by_user_id",
                table: "approved_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_freebies_users_approved_by",
                table: "approved_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_companies_users_added_by",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_users_added_by",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_freebie_requests_users_added_by",
                table: "freebie_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_items_users_added_by",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_users_added_by",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_meat_types_users_added_by",
                table: "meat_types");

            migrationBuilder.DropForeignKey(
                name: "fk_product_categories_users_added_by",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_product_sub_categories_users_added_by",
                table: "product_sub_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_users_added_by",
                table: "rejected_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_uoms_users_added_by",
                table: "uoms");

            migrationBuilder.DropForeignKey(
                name: "fk_user_roles_users_added_by",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_approved_freebies_freebies_freebies_id",
                table: "approved_freebies");

            migrationBuilder.DropForeignKey(
                name: "fk_rejected_freebies_freebies_freebies_id",
                table: "rejected_freebies");

            migrationBuilder.DropTable(
                name: "client_documents");

            migrationBuilder.DropTable(
                name: "freebie_request");

            migrationBuilder.DropTable(
                name: "permit");

            migrationBuilder.DropTable(
                name: "rejected_clients");

            migrationBuilder.DropTable(
                name: "requested_clients");

            migrationBuilder.DropTable(
                name: "approvals");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "fixed_discounts");

            migrationBuilder.DropTable(
                name: "variable_discounts");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "term_days");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "freebies");

            migrationBuilder.DropTable(
                name: "approved_freebies");

            migrationBuilder.DropTable(
                name: "freebie_requests");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "rejected_freebies");

            migrationBuilder.DropTable(
                name: "approved_clients");

            migrationBuilder.DropTable(
                name: "meat_types");

            migrationBuilder.DropTable(
                name: "product_sub_categories");

            migrationBuilder.DropTable(
                name: "uoms");

            migrationBuilder.DropTable(
                name: "product_categories");
        }
    }
}
