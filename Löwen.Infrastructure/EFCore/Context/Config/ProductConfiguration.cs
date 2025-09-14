using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// Product Configuration
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        builder.Property(p => p.Description).IsRequired().HasColumnType("text");
        builder.Property(p => p.Price).IsRequired().HasColumnType("numeric(18, 2)");
        builder.Property(p => p.StockQuantity).IsRequired().HasColumnType("smallint");
        builder.Property(p => p.Status).IsRequired().HasColumnType("smallint");

        // One-to-many relationship with ProductCategory
        builder.HasOne(p => p.Category)
               .WithMany(pc => pc.Products)
               .HasForeignKey(p => p.CategoryId)
               .IsRequired();

        // One-to-many relationship with ProductTag
        builder.HasOne(pt => pt.Tag)
               .WithOne(p => p.Product)
               .HasForeignKey<ProductTag>(p => p.ProductId)
               .IsRequired(false);
    }
}
