using Löwen.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Löwen.Infrastructure.EFCore.IdentityUser;

public class AppUser : IdentityUser<Guid>
{
    public string? FName { get; set; }
    public string? MName { get; set; }
    public string? LName { get; set; }
    public char Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ImagePath { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    //this field simple in future the customer can have many address and i already made CustomerAddress for this 
    //but for v1 not impelemented
    public string? AddressDetails {  get; set; }

    public ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = [];
    public ICollection<DeliveryOrder> DeliveryOrders { get; set; } = [];
    public ICollection<CustomerAddress> CustomerAddresses { get; set; } = [];
    public ICollection<Cart> Carts { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<ProductReview> ProductReviews { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<AdminLog> AdminLogs { get; set; } = [];
    public ICollection<Wishlist> Wishlists { get; set; } = [];
    public ICollection<LoveProductUser> Loves { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];
}



