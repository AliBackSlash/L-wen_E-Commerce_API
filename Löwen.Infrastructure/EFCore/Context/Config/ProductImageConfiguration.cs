using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// ProductImage Configuration (Junction Table)
public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        // Composite primary key
        builder.HasKey(pi => new { pi.ProductVariantId, pi.ImageId });

        // Column types
        builder.Property(pi => pi.ProductVariant).HasColumnType("uuid");
        builder.Property(pi => pi.ImageId).HasColumnType("uuid");

        // Relationships
        builder.HasOne(pi => pi.ProductVariant)
               .WithMany(p => p.ProductImages)
               .HasForeignKey(pi => pi.ProductVariantId)
               .IsRequired();

        builder.HasOne(pi => pi.Image)
               .WithMany(i => i.ProductImages)
               .HasForeignKey(pi => pi.ImageId)
               .IsRequired();
    }
}
