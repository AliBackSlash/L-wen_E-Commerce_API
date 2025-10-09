using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategryDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Color_ColorId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Size_SizeId",
                table: "ProductVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "ProductVariant",
                newName: "ProductVariants");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_SizeId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ColorId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Category", "Gender" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Men", 'M' },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Women", 'F' },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Kids", 'U' },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Accessories", 'U' },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Footwear", 'U' }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId_ColorId",
                table: "ProductVariants",
                columns: new[] { "ProductId", "ColorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId_SizeId",
                table: "ProductVariants",
                columns: new[] { "ProductId", "SizeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Color_ColorId",
                table: "ProductVariants",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Size_SizeId",
                table: "ProductVariants",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Color_ColorId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Size_SizeId",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ProductId_ColorId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ProductId_SizeId",
                table: "ProductVariants");

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "ProductVariant");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_SizeId",
                table: "ProductVariant",
                newName: "IX_ProductVariant_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ColorId",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Color_ColorId",
                table: "ProductVariant",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Size_SizeId",
                table: "ProductVariant",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
