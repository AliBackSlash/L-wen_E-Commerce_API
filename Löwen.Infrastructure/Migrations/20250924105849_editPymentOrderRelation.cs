using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editPymentOrderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Orders_OrderId1",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_OrderId1",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "Payment",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId1",
                table: "Payment",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Orders_OrderId1",
                table: "Payment",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
