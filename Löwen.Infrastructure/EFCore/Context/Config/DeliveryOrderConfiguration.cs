using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

public class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
{
    public void Configure(EntityTypeBuilder<DeliveryOrder> builder)
    {
        // Composite primary key
        builder.HasKey(po => new { po.DeliveryId, po.OrderId });

        // Column types
        builder.Property(po => po.DeliveryId).HasColumnType("uuid");
        builder.Property(po => po.OrderId).HasColumnType("uuid");

        // Relationships
        builder.HasOne(pd => pd.Order)
               .WithMany(p => p.DeliveryOrders) 
               .HasForeignKey(pd => pd.OrderId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}