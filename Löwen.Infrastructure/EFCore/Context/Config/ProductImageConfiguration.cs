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
        builder.HasKey(pi => new { pi.ProductId, pi.ImageId });

        // Column types
        builder.Property(pi => pi.ProductId).HasColumnType("uuid");
        builder.Property(pi => pi.ImageId).HasColumnType("uuid");

        // Relationships
        builder.HasOne(pi => pi.Product)
               .WithMany(p => p.ProductImages)
               .HasForeignKey(pi => pi.ProductId)
               .IsRequired();

        builder.HasOne(pi => pi.Image)
               .WithMany(i => i.ProductImages)
               .HasForeignKey(pi => pi.ImageId)
               .IsRequired();
    }
}
