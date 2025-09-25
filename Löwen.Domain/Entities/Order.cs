using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

// Orders Table
public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }

    // Navigation properties
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public Payment? Payment { get; set; }
    public ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();
}
