using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

public class Coupon
{
    public Guid Id { get; set; }   
    public string? Code { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal? DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public int? UsageLimit { get; set; }

    // 🔗 Relations
    public ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();
}
