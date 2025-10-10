namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models.ProductModels;

public class UpdateProductVariantModel
{
    public required string Id { get;  set; }
    public string? ColorId { get; set; }
    public string? SizeId { get; set; }
    public decimal? Price { get; set; }
    public int? StockQuantity { get; set; }
}
