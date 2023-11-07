﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class NullableApprovedByInListingFeeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_listing_fees_users_approved_by_user_id",
                table: "listing_fees");

            migrationBuilder.AlterColumn<int>(
                name: "approved_by",
                table: "listing_fees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_users_approved_by_user_id",
                table: "listing_fees",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_listing_fees_users_approved_by_user_id",
                table: "listing_fees");

            migrationBuilder.AlterColumn<int>(
                name: "approved_by",
                table: "listing_fees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_listing_fees_users_approved_by_user_id",
                table: "listing_fees",
                column: "approved_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}