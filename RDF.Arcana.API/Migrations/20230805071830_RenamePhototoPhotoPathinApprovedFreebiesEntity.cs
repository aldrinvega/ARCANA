using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RenamePhototoPhotoPathinApprovedFreebiesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_proof",
                table: "approved_freebies");

            migrationBuilder.AddColumn<string>(
                name: "photo_proof_path",
                table: "approved_freebies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_proof_path",
                table: "approved_freebies");

            migrationBuilder.AddColumn<byte[]>(
                name: "photo_proof",
                table: "approved_freebies",
                type: "longblob",
                nullable: true);
        }
    }
}
