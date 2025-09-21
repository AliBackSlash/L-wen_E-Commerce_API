using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_Discount_DiscountId",
                table: "ProductDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_Products_ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDiscount",
                table: "ProductDiscount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.RenameTable(
                name: "ProductDiscount",
                newName: "ProductDiscounts");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscount_DiscountId",
                table: "ProductDiscounts",
                newName: "IX_ProductDiscounts_DiscountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDiscounts",
                table: "ProductDiscounts",
                columns: new[] { "ProductId", "DiscountId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Discounts_DiscountId",
                table: "ProductDiscounts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Products_ProductId",
                table: "ProductDiscounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Discounts_DiscountId",
                table: "ProductDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Products_ProductId",
                table: "ProductDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDiscounts",
                table: "ProductDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "ProductDiscounts",
                newName: "ProductDiscount");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscounts_DiscountId",
                table: "ProductDiscount",
                newName: "IX_ProductDiscount_DiscountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDiscount",
                table: "ProductDiscount",
                columns: new[] { "ProductId", "DiscountId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_Discount_DiscountId",
                table: "ProductDiscount",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_Products_ProductId",
                table: "ProductDiscount",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
