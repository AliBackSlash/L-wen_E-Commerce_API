using Löwen.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Löwen.Infrastructure.EFCore.IdentityUser;

public class AppUser : IdentityUser<Guid>
{
    public int Id { get; set; }
    public string? FName { get; set; }
    public string? MName { get; set; }
    public string? LName { get; set; }
    public string? ImagePath { get; set; }
    
    public ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    public ICollection<AdminLog> AdminLogs { get; set; } = new List<AdminLog>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}



