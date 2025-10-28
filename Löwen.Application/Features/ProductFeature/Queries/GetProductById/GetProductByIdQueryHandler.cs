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
        var result = await productService.GetProductByIdAsync(Guid.Parse(query.productId), ct);

        if (result.IsFailure)
            return Result.Failure<GetProductByIdQueryResponse>(result.Errors);

        return Result.Success(new GetProductByIdQueryResponse
        {
            Name = result.Value.Name,
            Description = result.Value.Description,
            LoveCount = result.Value.LoveCount,
            Price = result.Value.Price,
            PriceAfterDiscount = result.Value.PriceAfterDiscount,
            ProductImage = result.Value.ProductImage,
            Status = result.Value.Status,
            Rating = result.Value.Rating,
            Reviews = result.Value.Reviews.Select(x => new ProductReviewsRe
            {
                UserName = x.UserName,
                UserImage = x.UserImage,
                Rating = x.Rating,
                Review = x.Review,
                CreatedAt = x.CreatedAt, 
            })
        });
    }
}
