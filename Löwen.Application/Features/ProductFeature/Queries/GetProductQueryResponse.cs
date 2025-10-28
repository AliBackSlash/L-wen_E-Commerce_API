using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;

namespace Löwen.Application.Features.ProductFeature.Queries;

public class GetProductQueryResponse
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public double LoveCount { get; set; }
    public decimal? PriceAfterDiscount {  get; set; }
    public double? Rating { get; set; }
    public string? ProductImage { get; set; }

    private GetProductQueryResponse() { }

    public static GetProductQueryResponse Map(GetProductDto dto) => new GetProductQueryResponse
    {
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price,
        Status = dto.Status,
        LoveCount = dto.LoveCount,
        PriceAfterDiscount = dto.Discount,
        ProductImage = dto.ProductImages,
        Rating = dto.Rating,

    };

    public static IEnumerable<GetProductQueryResponse> Map(IEnumerable<GetProductDto> dto)
    {
        if (dto == null)
            return [];

        return dto.Select(x => Map(x));
    }
}


