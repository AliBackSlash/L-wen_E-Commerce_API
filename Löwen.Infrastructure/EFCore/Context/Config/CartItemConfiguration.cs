using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// CartItem Configuration (Junction Table)
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        // Composite primary key
        builder.HasKey(ci => new { ci.CartId, ci.ProductId });

        // Column types
        builder.Property(ci => ci.CartId).HasColumnType("uuid");
        builder.Property(ci => ci.ProductId).HasColumnType("uuid");
        builder.Property(ci => ci.Quantity).IsRequired().HasColumnType("smallint");
        builder.Property(c => c.CreatedAt)
              .IsRequired()
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");
        // Relationships
        builder.HasOne(ci => ci.Cart)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.CartId)
               .IsRequired();

        builder.HasOne(ci => ci.Product)
               .WithMany(p => p.CartItems)
               .HasForeignKey(ci => ci.ProductId)
               .IsRequired();

    }
}
