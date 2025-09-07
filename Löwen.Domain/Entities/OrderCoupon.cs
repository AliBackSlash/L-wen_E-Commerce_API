namespace Löwen.Domain.Entities;

public class OrderCoupon
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Guid CouponId { get; set; }
    public Coupon Coupon { get; set; } = null!;
}
