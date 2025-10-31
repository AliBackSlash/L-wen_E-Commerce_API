namespace Löwen.Domain.Layer_Dtos.Product;

public class GetProductsDto
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public  double LoveCount { get; set; }
    public required string CreatedBy { get; set; }
    public double MainPrice { get; set; }
    public ProductStatus Status { get; set; }

}