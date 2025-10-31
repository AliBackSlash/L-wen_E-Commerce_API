using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// Wishlist Configuration (Junction Table)
public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        // Composite primary key
        builder.HasKey(w => new { w.UserId, w.ProductId });

        // Column types
        builder.Property(w => w.UserId).HasColumnType("uuid");
        builder.Property(w => w.ProductId).HasColumnType("uuid");
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

        // Relationships

        builder.HasOne(w => w.Product)
               .WithMany(p => p.Wishlists)
               .HasForeignKey(w => w.ProductId)
               .IsRequired();
    }
}
public class LoveProductUserConfiguration : IEntityTypeConfiguration<LoveProductUser>
{
    public void Configure(EntityTypeBuilder<LoveProductUser> builder)
    {
        // Composite primary key
        builder.HasKey(w => new { w.UserId, w.ProductId });

        // Column types
        builder.Property(w => w.UserId).HasColumnType("uuid");
        builder.Property(w => w.ProductId).HasColumnType("uuid");

        // Relationships

        builder.HasOne(w => w.Product)
               .WithMany(p => p.Loves)
               .HasForeignKey(w => w.ProductId)
               .IsRequired();
    }
}
