using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(p => p.Price).IsRequired().HasColumnType("numeric(18, 2)");
        builder.Property(p => p.StockQuantity).IsRequired().HasColumnType("smallint");

        builder.HasIndex(x => new { x.ProductId, x.SizeId }).IsUnique();
        builder.HasIndex(x => new { x.ProductId, x.ColorId }).IsUnique();

        builder.HasOne(p => p.Product)
            .WithMany(x => x.ProductVariants)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.HasOne(p => p.Color)
            .WithMany(x => x.ProductVariants)
            .HasForeignKey(x => x.ColorId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(p => p.Size)
            .WithMany(x => x.ProductVariants)
            .HasForeignKey(x => x.SizeId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}
