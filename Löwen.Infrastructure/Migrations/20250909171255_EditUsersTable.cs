using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Gender",
                table: "AspNetUsers",
                type: "Char(1)",
                nullable: false,
                defaultValue: 'M',
                oldClrType: typeof(char),
                oldType: "Char(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Gender",
                table: "AspNetUsers",
                type: "Char(1)",
                nullable: false,
                oldClrType: typeof(char),
                oldType: "Char(1)",
                oldDefaultValue: 'M');
        }
    }
}
