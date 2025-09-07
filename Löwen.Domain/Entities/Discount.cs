using Löwen.Domain.Enums;

namespace Löwen.Domain.Entities;

public class Discount
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; // e.g. "Ramadan Sale"
    public DiscountType DiscountType { get; set; }
    public decimal? DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();
}
