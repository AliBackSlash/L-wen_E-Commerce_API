using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// ProductDiscount Configuration (Junction Table)
public class ProductDiscountConfiguration : IEntityTypeConfiguration<ProductDiscount>
{
    public void Configure(EntityTypeBuilder<ProductDiscount> builder)
    {
        // Composite primary key
        builder.HasKey(pd => new { pd.ProductId, pd.DiscountId });

        // Column types
        builder.Property(pd => pd.ProductId).HasColumnType("uuid");
        builder.Property(pd => pd.DiscountId).HasColumnType("uuid");

        // Relationships
        builder.HasOne(pd => pd.Product)
               .WithMany(p => p.ProductDiscounts) 
               .HasForeignKey(pd => pd.ProductId)
               .IsRequired();

        builder.HasOne(pd => pd.Discount)
               .WithMany(d => d.ProductDiscounts)
               .HasForeignKey(pd => pd.DiscountId)
               .IsRequired();
    }
}
public class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
{
    public void Configure(EntityTypeBuilder<DeliveryOrder> builder)
    {
        // Composite primary key
        builder.HasKey(po => new { po.DeliveryId, po.OrderId });

        // Column types
        builder.Property(po => po.DeliveryId).HasColumnType("uuid");
        builder.Property(po => po.OrderId).HasColumnType("uuid");

        // Relationships
        builder.HasOne(pd => pd.Order)
               .WithMany(p => p.DeliveryOrders) 
               .HasForeignKey(pd => pd.OrderId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}