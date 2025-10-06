namespace Löwen.Domain.Layer_Dtos.Product;

public class ProductVariantDto
{
    public Guid ColorId { get; set; }
    public Guid? SizeId { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
