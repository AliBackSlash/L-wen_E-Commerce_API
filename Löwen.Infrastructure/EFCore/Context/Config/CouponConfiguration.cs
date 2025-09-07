using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// Coupon Configuration
public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()"); ;

        builder.Property(c => c.Code).HasMaxLength(50).HasColumnType("varchar");
        builder.HasIndex(c => c.Code).IsUnique(); 

        builder.Property(c => c.DiscountType).IsRequired().HasColumnType("smallint");
        builder.Property(c => c.DiscountValue).HasColumnType("numeric(18, 2)");
        builder.Property(c => c.StartDate)
               .IsRequired()
               .HasColumnType("timestamp with time zone");
        builder.Property(c => c.EndDate)
               .IsRequired()
               .HasColumnType("timestamp with time zone");
        builder.Property(c => c.IsActive).IsRequired().HasColumnType("boolean").HasDefaultValueSql("true"); ;
        builder.Property(c => c.UsageLimit).HasColumnType("integer");

        // Many-to-many relationship with Order (via OrderCoupon)
        builder.HasMany(c => c.OrderCoupons)
               .WithOne(oc => oc.Coupon)
               .HasForeignKey(oc => oc.CouponId)
               .IsRequired();
    }
}
