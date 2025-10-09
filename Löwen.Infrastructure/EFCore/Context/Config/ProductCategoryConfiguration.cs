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

        builder.HasData(FillData());
    }

    private List<ProductCategory> FillData()
      => new()
      {
          new ProductCategory
          {
              Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
              Category = "Men",
              Gender = 'M'
          },
          new ProductCategory
          {
              Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
              Category = "Women",
              Gender = 'F'
          },
          new ProductCategory
          {
              Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
              Category = "Kids",
              Gender = 'U'
          },
          new ProductCategory
          {
              Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
              Category = "Accessories",
              Gender = 'U'
          },
          new ProductCategory
          {
              Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
              Category = "Footwear",
              Gender = 'U'
          },
      };
}
