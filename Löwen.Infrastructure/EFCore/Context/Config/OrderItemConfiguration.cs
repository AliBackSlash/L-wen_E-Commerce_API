using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// OrderItem Configuration (Junction Table)
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // Composite primary key
        builder.HasKey(oi => new { oi.OrderId, oi.ProductId });

        // Column types
        builder.Property(oi => oi.OrderId).HasColumnType("uuid");
        builder.Property(oi => oi.ProductId).HasColumnType("uuid");
        builder.Property(oi => oi.Quantity).IsRequired().HasColumnType("smallint");
        builder.Property(oi => oi.PriceAtPurchase).IsRequired().HasColumnType("numeric(18, 2)");

        // Relationships
        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId)
               .IsRequired();

        builder.HasOne(oi => oi.Product)
               .WithMany(p => p.OrderItems)
               .HasForeignKey(oi => oi.ProductId)
               .IsRequired();


    }
}
