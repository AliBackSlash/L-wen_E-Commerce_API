using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

// Orders Table
public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }// => Delivery is a normal user with role Delivery
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }

    // Navigation properties
    public Payment? Payment { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ICollection<OrderCoupon> OrderCoupons { get; set; } = [];
    public ICollection<DeliveryOrder> DeliveryOrders { get; set; } = [];
}
