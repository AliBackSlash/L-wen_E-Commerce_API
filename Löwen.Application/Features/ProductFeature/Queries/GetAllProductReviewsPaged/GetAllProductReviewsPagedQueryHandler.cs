using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductReviewsPaged;

public class GetAllProductReviewsPagedQueryHandler(IProductService productService,IOptions<PaginationSettings> options)
    : IQueryHandler<GetAllProductReviewsPagedQuery, PagedResult<ProductReviewsResponse>>
{
    public async Task<Result<PagedResult<ProductReviewsResponse>>> Handle(GetAllProductReviewsPagedQuery query, CancellationToken ct)
    {
        var result = await productService.GetProductReviewsPaged(Guid.Parse(query.productId),new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            Take = query.PageSize,
            PageNumber = query.PageNumber
        }, ct);

        if (result.IsFailure)
            return Result.Failure<PagedResult<ProductReviewsResponse>>(result.Errors);

        return Result.Success(PagedResult<ProductReviewsResponse>.Create(result.Value.Items.Select(x => new ProductReviewsResponse 
        { 
            UserName = x.UserName,
            UserImage = x.UserImage,
            Review = x.Review,
            Rating = x.Rating,
            CreatedAt = x.CreatedAt,
            
        }).ToList()
            ,result.Value.TotalCount,result.Value.PageNumber,result.Value.PageSize));
    }
}
