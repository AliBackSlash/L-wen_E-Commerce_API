using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

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
