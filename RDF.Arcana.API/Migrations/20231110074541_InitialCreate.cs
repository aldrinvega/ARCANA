using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "arcana_hilo_sequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    house_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    barangay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "business_address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    house_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    barangay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "variable_discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    minimum_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    maximum_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    minimum_percentage = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    maximum_percentage = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    is_subject_to_approval = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_variable_discounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "approvals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    approval_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    requested_by = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approvals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_coverages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_coverage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_coverages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    document_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    document_path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    owners_address_id = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    business_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tin_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    representative_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    representative_position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    business_address_id = table.Column<int>(type: "int", nullable: true),
                    cluster = table.Column<int>(type: "int", nullable: false),
                    freezer = table.Column<bool>(type: "bit", nullable: false),
                    customer_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    term_days = table.Column<int>(type: "int", nullable: true),
                    discount_id = table.Column<int>(type: "int", nullable: true),
                    client_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    store_type_id = table.Column<int>(type: "int", nullable: true),
                    registration_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    terms = table.Column<int>(type: "int", nullable: true),
                    mode_of_payment = table.Column<int>(type: "int", nullable: true),
                    direct_delivery = table.Column<bool>(type: "bit", nullable: true),
                    booking_coverage_id = table.Column<int>(type: "int", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fixed_discount_id = table.Column<int>(type: "int", nullable: true),
                    variable_discount = table.Column<bool>(type: "bit", nullable: true),
                    variable_discounts_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_address_owners_address_id",
                        column: x => x.owners_address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_clients_booking_coverages_booking_coverages_id",
                        column: x => x.booking_coverage_id,
                        principalTable: "booking_coverages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clients_business_address_business_address_id",
                        column: x => x.business_address_id,
                        principalTable: "business_address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clients_variable_discounts_variable_discounts_id",
                        column: x => x.variable_discounts_id,
                        principalTable: "variable_discounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "fixed_discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    discount_percentage = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fixed_discounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_fixed_discounts_clients_clients_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lower_bound = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    upper_bound = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    commission_rate_lower = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    commission_rate_upper = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "freebie_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "freebie_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    approvals_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_delivered = table.Column<bool>(type: "bit", nullable: false),
                    photo_proof_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    e_signature_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requested_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_requests_approvals_approvals_id",
                        column: x => x.approvals_id,
                        principalTable: "approvals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_freebie_requests_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    item_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    product_sub_category_id = table.Column<int>(type: "int", nullable: false),
                    meat_type_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "listing_fee_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    listing_fee_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    sku = table.Column<int>(type: "int", nullable: false),
                    unit_cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listing_fee_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_listing_fee_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "listing_fees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    approvals_id = table.Column<int>(type: "int", nullable: false),
                    crated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_delivered = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requested_by = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listing_fees", x => x.id);
                    table.ForeignKey(
                        name: "fk_listing_fees_approvals_approvals_id",
                        column: x => x.approvals_id,
                        principalTable: "approvals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_listing_fees_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    location_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meat_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    meat_type_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meat_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mode_of_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mode_of_payments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_category_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_sub_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_sub_category_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_category_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sub_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_sub_categories_product_categories_product_category_id",
                        column: x => x.product_category_id,
                        principalTable: "product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "store_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    store_type_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_store_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "term_days",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    days = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_term_days", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "term_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    terms_id = table.Column<int>(type: "int", nullable: false),
                    credit_limit = table.Column<int>(type: "int", nullable: true),
                    term_days_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    clients_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_term_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_term_options_clients_clients_id",
                        column: x => x.clients_id,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_term_options_term_days_term_days_id",
                        column: x => x.term_days_id,
                        principalTable: "term_days",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    term_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_terms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "uoms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uom_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uom_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uoms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_role_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_id_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_password_changed = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    location_id = table.Column<int>(type: "int", nullable: true),
                    user_roles_id = table.Column<int>(type: "int", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    profile_picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        name: "fk_users_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_user_roles_user_roles_id",
                        column: x => x.user_roles_id,
                        principalTable: "user_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_users_added_by",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_approvals_approved_by",
                table: "approvals",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_client_id",
                table: "approvals",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_approvals_requested_by",
                table: "approvals",
                column: "requested_by");

            migrationBuilder.CreateIndex(
                name: "ix_booking_coverages_added_by",
                table: "booking_coverages",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_client_documents_client_id",
                table: "client_documents",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_added_by",
                table: "clients",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_booking_coverage_id",
                table: "clients",
                column: "booking_coverage_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_business_address_id",
                table: "clients",
                column: "business_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_fixed_discount_id",
                table: "clients",
                column: "fixed_discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_mode_of_payment",
                table: "clients",
                column: "mode_of_payment");

            migrationBuilder.CreateIndex(
                name: "ix_clients_modified_by",
                table: "clients",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_clients_owners_address_id",
                table: "clients",
                column: "owners_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_store_type_id",
                table: "clients",
                column: "store_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_terms",
                table: "clients",
                column: "terms");

            migrationBuilder.CreateIndex(
                name: "ix_clients_user_id",
                table: "clients",
                column: "user_id");

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
                name: "ix_fixed_discounts_client_id",
                table: "fixed_discounts",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_items_item_id",
                table: "freebie_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_items_request_id",
                table: "freebie_items",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_approvals_id",
                table: "freebie_requests",
                column: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_client_id",
                table: "freebie_requests",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_requests_requested_by",
                table: "freebie_requests",
                column: "requested_by");

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
                name: "ix_listing_fee_items_item_id",
                table: "listing_fee_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fee_items_listing_fee_id",
                table: "listing_fee_items",
                column: "listing_fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_approvals_id",
                table: "listing_fees",
                column: "approvals_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_approved_by",
                table: "listing_fees",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_client_id",
                table: "listing_fees",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_fees_requested_by",
                table: "listing_fees",
                column: "requested_by");

            migrationBuilder.CreateIndex(
                name: "ix_locations_added_by",
                table: "locations",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_meat_types_added_by",
                table: "meat_types",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_mode_of_payments_added_by",
                table: "mode_of_payments",
                column: "added_by");

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
                name: "ix_store_types_added_by",
                table: "store_types",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_store_types_modified_by",
                table: "store_types",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "ix_term_days_added_by",
                table: "term_days",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_added_by",
                table: "term_options",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_clients_id",
                table: "term_options",
                column: "clients_id");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_term_days_id",
                table: "term_options",
                column: "term_days_id");

            migrationBuilder.CreateIndex(
                name: "ix_term_options_terms_id",
                table: "term_options",
                column: "terms_id");

            migrationBuilder.CreateIndex(
                name: "ix_terms_added_by",
                table: "terms",
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
                column: "added_by",
                filter: "[added_by] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_users_company_id",
                table: "users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_department_id",
                table: "users",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_location_id",
                table: "users",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_roles_id",
                table: "users",
                column: "user_roles_id");

            migrationBuilder.AddForeignKey(
                name: "fk_approvals_clients_client_id",
                table: "approvals",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_approvals_users_approve_by_user_id",
                table: "approvals",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_approvals_users_requested_by_user_id",
                table: "approvals",
                column: "requested_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_booking_coverages_users_added_by",
                table: "booking_coverages",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_client_documents_clients_clients_id",
                table: "client_documents",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_fixed_discounts_fixed_discounts_id",
                table: "clients",
                column: "fixed_discount_id",
                principalTable: "fixed_discounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_mode_of_payments_mode_of_payments_id",
                table: "clients",
                column: "mode_of_payment",
                principalTable: "mode_of_payments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_store_types_store_type_id",
                table: "clients",
                column: "store_type_id",
                principalTable: "store_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_clients_term_options_term_id",
                table: "clients",
                column: "terms",
                principalTable: "term_options",
                principalColumn: "id");

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_users_user_id",
                table: "clients",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_companies_users_added_by",
                table: "companies",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_users_added_by",
                table: "departments",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_discounts_users_added_by",
                table: "discounts",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_items_freebie_requests_request_id",
                table: "freebie_items",
                column: "request_id",
                principalTable: "freebie_requests",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_items_items_item_id",
                table: "freebie_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_freebie_requests_users_requested_by_user_id",
                table: "freebie_requests",
                column: "requested_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_items_meat_types_meat_type_id",
                table: "items",
                column: "meat_type_id",
                principalTable: "meat_types",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_items_product_sub_categories_product_sub_category_id",
                table: "items",
                column: "product_sub_category_id",
                principalTable: "product_sub_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_items_uoms_uom_id",
                table: "items",
                column: "uom_id",
                principalTable: "uoms",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_items_users_added_by",
                table: "items",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fee_items_listing_fees_listing_fee_id",
                table: "listing_fee_items",
                column: "listing_fee_id",
                principalTable: "listing_fees",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_users_approved_by_user_id",
                table: "listing_fees",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_users_requested_by_user_id",
                table: "listing_fees",
                column: "requested_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_users_added_by",
                table: "locations",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_meat_types_users_added_by",
                table: "meat_types",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_mode_of_payments_users_added_by_user_id",
                table: "mode_of_payments",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_product_categories_users_added_by",
                table: "product_categories",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_product_sub_categories_users_added_by",
                table: "product_sub_categories",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_store_types_users_added_by_user_id",
                table: "store_types",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_store_types_users_modified_by",
                table: "store_types",
                column: "modified_by",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_term_days_users_added_by",
                table: "term_days",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_terms_terms_id",
                table: "term_options",
                column: "terms_id",
                principalTable: "terms",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_term_options_users_added_by_user_id",
                table: "term_options",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_terms_users_added_by_user_id",
                table: "terms",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_uoms_users_added_by",
                table: "uoms",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "fk_user_roles_users_added_by",
                table: "user_roles",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_fixed_discounts_clients_clients_id",
                table: "fixed_discounts");

            migrationBuilder.DropForeignKey(
                name: "fk_term_options_clients_clients_id",
                table: "term_options");

            migrationBuilder.DropForeignKey(
                name: "fk_companies_users_added_by",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_users_added_by",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_users_added_by",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_user_roles_users_added_by",
                table: "user_roles");

            migrationBuilder.DropTable(
                name: "client_documents");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "freebie_items");

            migrationBuilder.DropTable(
                name: "listing_fee_items");

            migrationBuilder.DropTable(
                name: "freebie_requests");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "listing_fees");

            migrationBuilder.DropTable(
                name: "meat_types");

            migrationBuilder.DropTable(
                name: "product_sub_categories");

            migrationBuilder.DropTable(
                name: "uoms");

            migrationBuilder.DropTable(
                name: "approvals");

            migrationBuilder.DropTable(
                name: "product_categories");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "booking_coverages");

            migrationBuilder.DropTable(
                name: "business_address");

            migrationBuilder.DropTable(
                name: "fixed_discounts");

            migrationBuilder.DropTable(
                name: "mode_of_payments");

            migrationBuilder.DropTable(
                name: "store_types");

            migrationBuilder.DropTable(
                name: "term_options");

            migrationBuilder.DropTable(
                name: "variable_discounts");

            migrationBuilder.DropTable(
                name: "term_days");

            migrationBuilder.DropTable(
                name: "terms");

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

            migrationBuilder.DropSequence(
                name: "arcana_hilo_sequence");
        }
    }
}
