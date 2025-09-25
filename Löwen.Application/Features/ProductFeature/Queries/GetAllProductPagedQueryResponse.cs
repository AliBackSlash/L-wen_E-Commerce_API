using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;

namespace Löwen.Application.Features.ProductFeature.Queries;

public class GetAllProductPagedQueryResponse
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public decimal? Discount {  get; set; }
    public double? Rating { get; set; }
    public string? ProductImages {  get; set; }

    private GetAllProductPagedQueryResponse() { }

    public static GetAllProductPagedQueryResponse Map(GetProductResult dto) => new GetAllProductPagedQueryResponse
    {
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price,
        Status = dto.Status,
        LoveCount = dto.LoveCount,
        Discount = dto.Discount,
        ProductImages = dto.ProductImages,
        Rating = dto.Rating,

    };

    public static IEnumerable<GetAllProductPagedQueryResponse> Map(IEnumerable<GetProductResult> dto)
    {
        if (dto == null)
            return [];

        return dto.Select(x => Map(x));
    }
}
