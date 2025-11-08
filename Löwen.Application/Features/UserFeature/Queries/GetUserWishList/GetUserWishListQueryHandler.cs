using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.UserFeature.Queries.GetUserWishList;

internal class GetUserWishListQueryHandler(IWishlistService wishlistService,IOptions<PaginationSettings> options)
    : IQueryHandler<GetUserWishListQuery, PagedResult<GetUserWishListQueryResponse>>
{
    public async Task<Result<PagedResult<GetUserWishListQueryResponse>>> Handle(GetUserWishListQuery query, CancellationToken ct)
    {
        var result = await wishlistService.GetUserWishlist(Guid.Parse(query.userId), new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            PageNumber = query.PageNumber,
            Take = query.PageSize
        }, ct);

        return  Result.Success(PagedResult<GetUserWishListQueryResponse>
            .Create(result.Items.Select(x => new GetUserWishListQueryResponse
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                Image = x.Image,
            }),result.TotalCount ,result.PageNumber,result.PageSize));
    }
}
