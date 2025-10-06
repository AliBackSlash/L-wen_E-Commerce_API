namespace Löwen.Domain.Layer_Dtos.Product;

public class AddProductImagesDto
{
    public Guid ProductId { get; set; }
    public required string Path { get; set; }
    public bool IsMain { get; set; };
}