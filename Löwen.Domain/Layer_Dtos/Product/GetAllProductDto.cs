namespace Löwen.Domain.Layer_Dtos.Product;
public class GetAllProductDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public decimal? Discount { get; set; }
    public double? Rating { get; set; }
    public string? ProductImages { get; set; }

}