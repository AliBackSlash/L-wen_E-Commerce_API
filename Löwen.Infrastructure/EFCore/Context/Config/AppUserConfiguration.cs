using Löwen.Infrastructure.EFCore.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;
// User Configuration
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.FName).HasMaxLength(50).HasColumnType("varchar");
        builder.Property(u => u.MName).HasMaxLength(50).HasColumnType("varchar");
        builder.Property(ca => ca.AddressDetails).IsRequired().HasColumnType("text");
        builder.Property(u => u.LName).HasMaxLength(50).HasColumnType("varchar");
        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15).HasColumnType("varchar");
        builder.Property(u => u.Gender).HasColumnType("Char(1)").HasDefaultValue("M");
        builder.Property(u => u.DateOfBirth).HasColumnType("Date");

        builder.Property(u => u.ImagePath).HasMaxLength(2048).HasColumnType("varchar");
        builder.Property(u => u.IsDeleted).HasColumnType("Boolean").HasDefaultValue(false);
        builder.Property(u => u.DeletedAt).HasColumnType("timestamp with time zone").IsRequired(false);

        // Relationships

        builder.HasMany(al => al.AdminLogs)
               .WithOne()
               .HasForeignKey(al => al.AdminId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasMany(c => c.Carts)
               .WithOne()
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasMany(ca => ca.CustomerAddresses)
               .WithOne()
               .HasForeignKey(ca => ca.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasMany(n => n.Notifications)
              .WithOne()
              .HasForeignKey(n => n.UserId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();

        builder.HasMany(o => o.Orders)
               .WithOne()
               .HasForeignKey(o => o.DeliveryId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasMany(pr => pr.ProductReviews)
               .WithOne()
               .HasForeignKey(pr => pr.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
       
        builder.HasMany(pr => pr.Products)
               .WithOne()
               .HasForeignKey(pr => pr.CreatedBy)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired();

        builder.HasMany(w => w.Wishlists)
               .WithOne()
               .HasForeignKey(w => w.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
       
        builder.HasMany(w => w.Loves)
               .WithOne()
               .HasForeignKey(w => w.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasMany(o => o.DeliveryOrders)
               .WithOne()
               .HasForeignKey(o => o.DeliveryId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();


    }
}
