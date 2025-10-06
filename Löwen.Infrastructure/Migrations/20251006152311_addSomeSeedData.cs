using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSomeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "HexCode", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "#FFFFFF", "White" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "#000000", "Black" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "#001F3F", "Navy" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "#808080", "Gray" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "#D3D3D3", "Light Gray" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "#36454F", "Charcoal" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "#FF0000", "Red" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "#800020", "Burgundy" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "#0074D9", "Blue" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "#87CEEB", "Sky Blue" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "#2ECC40", "Green" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "#708238", "Olive" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "#8B4513", "Brown" },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "#F5F5DC", "Beige" },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "#D2B48C", "Tan" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "#FFC0CB", "Pink" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "#800080", "Purple" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "#FFDB58", "Mustard" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "#008080", "Teal" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "#D4AF37", "Gold" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "SizeAsName", "SizeAsNumber" },
                values: new object[,]
                {
                    { new Guid("f1a1f6a1-0001-4a6b-9b1a-000000000001"), "XS", null },
                    { new Guid("f1a1f6a1-0002-4a6b-9b1a-000000000002"), "S", null },
                    { new Guid("f1a1f6a1-0003-4a6b-9b1a-000000000003"), "M", null },
                    { new Guid("f1a1f6a1-0004-4a6b-9b1a-000000000004"), "L", null },
                    { new Guid("f1a1f6a1-0005-4a6b-9b1a-000000000005"), "XL", null },
                    { new Guid("f1a1f6a1-0006-4a6b-9b1a-000000000006"), "XXL", null },
                    { new Guid("f1a1f6a1-0010-4a6b-9b1a-000000000010"), null, (byte)28 },
                    { new Guid("f1a1f6a1-0011-4a6b-9b1a-000000000011"), null, (byte)30 },
                    { new Guid("f1a1f6a1-0012-4a6b-9b1a-000000000012"), null, (byte)32 },
                    { new Guid("f1a1f6a1-0013-4a6b-9b1a-000000000013"), null, (byte)34 },
                    { new Guid("f1a1f6a1-0014-4a6b-9b1a-000000000014"), null, (byte)36 },
                    { new Guid("f1a1f6a1-0015-4a6b-9b1a-000000000015"), null, (byte)38 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000d"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000e"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000f"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0001-4a6b-9b1a-000000000001"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0002-4a6b-9b1a-000000000002"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0003-4a6b-9b1a-000000000003"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0004-4a6b-9b1a-000000000004"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0005-4a6b-9b1a-000000000005"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0006-4a6b-9b1a-000000000006"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0010-4a6b-9b1a-000000000010"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0011-4a6b-9b1a-000000000011"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0012-4a6b-9b1a-000000000012"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0013-4a6b-9b1a-000000000013"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0014-4a6b-9b1a-000000000014"));

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Id",
                keyValue: new Guid("f1a1f6a1-0015-4a6b-9b1a-000000000015"));
        }
    }
}
