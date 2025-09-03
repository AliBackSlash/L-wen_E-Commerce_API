using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// ProductCategory Configuration
public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(pc => pc.Category).IsRequired().HasMaxLength(100).HasColumnType("varchar");
        builder.Property(pc => pc.Gender).IsRequired().HasColumnType("char(1)");
        builder.Property(pc => pc.AgeFrom).IsRequired().HasColumnType("smallint");
        builder.Property(pc => pc.AgeTo).IsRequired().HasColumnType("smallint");
    }
}
