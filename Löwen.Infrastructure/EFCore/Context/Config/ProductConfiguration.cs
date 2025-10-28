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
        builder.Property(p => p.Name).IsRequired().HasMaxLength(70).HasColumnType("varchar");
        builder.Property(p => p.Tags).IsRequired(false).HasMaxLength(70).HasColumnType("varchar");
        builder.Property(p => p.Description).IsRequired().HasMaxLength(1200).HasColumnType("varchar");
        builder.Property(p => p.Status).IsRequired().HasColumnType("smallint");
        builder.Property(p => p.LoveCount).IsRequired().HasColumnType("double precision").HasDefaultValueSql("0");
        builder.Property(p => p.MainPrice).IsRequired().HasDefaultValueSql("0");
        builder.Property(x => x.CreatedBy).HasColumnType("uuid").IsRequired();
      
        // One-to-many relationship with ProductCategory
        builder.HasOne(p => p.Category)
               .WithMany(pc => pc.Products)
               .HasForeignKey(p => p.CategoryId)
               .IsRequired();

    }
}
