namespace Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
public class GetProductDto
{
    public Guid? ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public double? Discount { get; set; }
    public DiscountType? discountType { get; set; }
    public double? Rating { get { if (this.Rating is null) return 0; else return this.Rating; } set {this.Rating = value; } }
    public string? ProductImage { get; set; }

}