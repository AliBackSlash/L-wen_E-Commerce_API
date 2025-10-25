namespace Löwen.Domain.Entities;

public class Discount
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; 
    public DiscountType DiscountType { get; set; }
    public decimal? DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;
}
