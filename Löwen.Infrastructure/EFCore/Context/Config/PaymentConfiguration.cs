using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// Payment Configuration
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.OrderId).IsRequired().HasColumnType("uuid");

        builder.Property(p => p.Amount).IsRequired().HasColumnType("numeric(18, 2)");
        builder.Property(p => p.PaymentMethod).IsRequired().HasColumnType("smallint");
        builder.Property(p => p.TransactionId).HasMaxLength(100).HasColumnType("varchar");
        builder.Property(p => p.Status).IsRequired().HasColumnType("smallint");

        builder.Property(p => p.CreatedAt)
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");
      
        // Foreign key to Order
        builder.HasOne(p => p.Order)
               .WithOne(o => o.Payment)
               .HasForeignKey<Payment>(p => p.OrderId)
               .IsRequired();
    }
}
