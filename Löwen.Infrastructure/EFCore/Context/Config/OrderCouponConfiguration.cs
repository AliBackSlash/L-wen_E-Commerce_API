using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// OrderCoupon Configuration (Junction Table)
public class OrderCouponConfiguration : IEntityTypeConfiguration<OrderCoupon>
{
    public void Configure(EntityTypeBuilder<OrderCoupon> builder)
    {
        builder.HasKey(oc => new { oc.OrderId, oc.CouponId });

        builder.Property(oc => oc.OrderId).HasColumnType("uuid");
        builder.Property(oc => oc.CouponId).HasColumnType("uuid");

        // Relationships
        builder.HasOne(oc => oc.Order)
               .WithMany(o => o.OrderCoupons) 
               .HasForeignKey(oc => oc.OrderId)
               .IsRequired();

        builder.HasOne(oc => oc.Coupon)
               .WithMany(c => c.OrderCoupons)
               .HasForeignKey(oc => oc.CouponId)
               .IsRequired();
    }
}
