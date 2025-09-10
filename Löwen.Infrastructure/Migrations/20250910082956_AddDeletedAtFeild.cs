using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtFeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");
        }
    }
}
