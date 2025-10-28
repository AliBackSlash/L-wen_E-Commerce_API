using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
using Löwen.Infrastructure.EFCore.Context.Config;
using Löwen.Infrastructure.EFCore.IdentityUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.EFCore.Context;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<DeliveryOrder> DeliveryOrders => Set<DeliveryOrder>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<Wishlist> Wishlist => Set<Wishlist>();
    public DbSet<LoveProductUser> LovesProductUser => Set<LoveProductUser>();
    public DbSet<AdminLog> AdminLogs => Set<AdminLog>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<OrderCoupon> OrderCoupons => Set<OrderCoupon>();



    #region Entities for map functions result
    public DbSet<GetProductDto> getProductResults => Set<GetProductDto>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        modelBuilder.Entity<GetProductDto>().HasNoKey().ToView(null);
        base.OnModelCreating(modelBuilder);

    }
}
