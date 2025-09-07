using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// Discount Configuration
public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(d => d.Name).IsRequired().HasMaxLength(100).HasColumnType("varchar");
        builder.Property(d => d.DiscountType).IsRequired().HasColumnType("smallint");
        builder.Property(d => d.DiscountValue).HasColumnType("numeric(18, 2)");
        builder.Property(d => d.StartDate)
               .IsRequired()
               .HasColumnType("timestamp with time zone");
        builder.Property(d => d.EndDate)
               .IsRequired()
               .HasColumnType("timestamp with time zone");
        builder.Property(d => d.IsActive).IsRequired().HasColumnType("boolean").HasDefaultValueSql("true");

        // Many-to-many relationship with Product (via ProductDiscount)
        builder.HasMany(d => d.ProductDiscounts)
               .WithOne(pd => pd.Discount)
               .HasForeignKey(pd => pd.DiscountId)
               .IsRequired();
    }
}
