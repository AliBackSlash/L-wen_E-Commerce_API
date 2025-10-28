namespace Löwen.Domain.Layer_Dtos.Discount;

public class DiscountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DiscountType DiscountType { get; set; }
    public double? DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;
}
