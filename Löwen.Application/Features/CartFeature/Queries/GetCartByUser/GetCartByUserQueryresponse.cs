using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.Layer_Dtos.Cart;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

public class GetCartByUserQueryresponse
{
    public required string ProductImageUrl {  get; set; }
    public required string ProductName {  get; set; }
    public decimal Price { get; set; }
    public short Quantity {  get; set; }
    public decimal PriceTotal { get { return Price * Quantity; } }

    private GetCartByUserQueryresponse() { }

    public static GetCartByUserQueryresponse map(GetCartItemDto dto)
        => new GetCartByUserQueryresponse
        { 
            ProductImageUrl = dto.ProductImageUrl,
            ProductName = dto.ProductName,
            Price = dto.Price,
            Quantity = dto.Quantity,
        };

    public static IEnumerable<GetCartByUserQueryresponse> map(IEnumerable<GetCartItemDto> dtos) => dtos.Select(i => map(i)).ToList();

}
