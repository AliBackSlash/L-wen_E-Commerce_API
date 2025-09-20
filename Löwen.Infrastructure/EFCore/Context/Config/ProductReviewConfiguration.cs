using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// ProductReview Configuration
public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.HasKey(pr => pr.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(pr => pr.Rating).IsRequired().HasColumnType("Char(1)");
        builder.Property(pr => pr.Review).IsRequired().HasMaxLength(1000).HasColumnType("VARCHAR");
        builder.Property(pr => pr.CreatedAt)
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

        // Relationships
        builder.HasOne(pr => pr.Product)
               .WithMany(p => p.ProductReviews)
               .HasForeignKey(pr => pr.ProductId)
               .IsRequired();

        
    }
}
