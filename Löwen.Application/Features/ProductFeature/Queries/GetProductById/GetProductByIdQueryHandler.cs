using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.ProductFeature.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductService productService)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    public async Task<Result<GetProductByIdQueryResponse>> Handle(GetProductByIdQuery query, CancellationToken ct)
    {
        Guid PId = Guid.Parse(query.productId);
        var result = await productService.GetProductByIdAsync(PId, ct);

        if (result.IsFailure)
            return Result.Failure<GetProductByIdQueryResponse>(result.Errors);

        return Result.Success(new GetProductByIdQueryResponse
        {
            ProductId = PId,
            Name = result.Value.Name,
            Description = result.Value.Description,
            LoveCount = result.Value.LoveCount,
            Price = result.Value.Price,
            Discount = result.Value.Discount,
            discountType = result.Value.DiscountType,
            ProductImage = result.Value.ProductImage,
            Status = result.Value.Status,
            Rating = result.Value.Rating,
            
        });
    }

    
}
