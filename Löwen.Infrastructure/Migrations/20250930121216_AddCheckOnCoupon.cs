using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckOnCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupon_Coupon_CouponId",
                table: "OrderCoupon");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupon_Orders_OrderId",
                table: "OrderCoupon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCoupon",
                table: "OrderCoupon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupon",
                table: "Coupon");

            migrationBuilder.RenameTable(
                name: "OrderCoupon",
                newName: "OrderCoupons");

            migrationBuilder.RenameTable(
                name: "Coupon",
                newName: "Coupons");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCoupon_CouponId",
                table: "OrderCoupons",
                newName: "IX_OrderCoupons_CouponId");

            migrationBuilder.RenameIndex(
                name: "IX_Coupon_Code",
                table: "Coupons",
                newName: "IX_Coupons_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCoupons",
                table: "OrderCoupons",
                columns: new[] { "OrderId", "CouponId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Coupon_StartDate_EndDate",
                table: "Coupons",
                sql: "\"StartDate\" < \"EndDate\"");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupons_Coupons_CouponId",
                table: "OrderCoupons",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupons_Orders_OrderId",
                table: "OrderCoupons",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupons_Coupons_CouponId",
                table: "OrderCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupons_Orders_OrderId",
                table: "OrderCoupons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCoupons",
                table: "OrderCoupons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Coupon_StartDate_EndDate",
                table: "Coupons");

            migrationBuilder.RenameTable(
                name: "OrderCoupons",
                newName: "OrderCoupon");

            migrationBuilder.RenameTable(
                name: "Coupons",
                newName: "Coupon");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCoupons_CouponId",
                table: "OrderCoupon",
                newName: "IX_OrderCoupon_CouponId");

            migrationBuilder.RenameIndex(
                name: "IX_Coupons_Code",
                table: "Coupon",
                newName: "IX_Coupon_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCoupon",
                table: "OrderCoupon",
                columns: new[] { "OrderId", "CouponId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupon",
                table: "Coupon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupon_Coupon_CouponId",
                table: "OrderCoupon",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupon_Orders_OrderId",
                table: "OrderCoupon",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
