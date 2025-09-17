using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAgesFromCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeFrom",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "AgeTo",
                table: "ProductCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "AgeFrom",
                table: "ProductCategories",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "AgeTo",
                table: "ProductCategories",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
