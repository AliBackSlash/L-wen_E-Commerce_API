using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "DeliveryId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
