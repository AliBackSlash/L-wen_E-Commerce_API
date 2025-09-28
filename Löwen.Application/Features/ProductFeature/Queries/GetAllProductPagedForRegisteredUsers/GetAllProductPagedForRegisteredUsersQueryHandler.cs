using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedForRegisteredUsers;

public class GetAllProductPagedForRegisteredUsersQueryHandler(IProductService productService,IOptions<PaginationSettings> options)
    : IQueryHandler<GetAllProductPagedForRegisteredUsersQuery, PagedResult<GetAllProductPagedQueryResponse>>
{
    public async Task<Result<PagedResult<GetAllProductPagedQueryResponse>>> Handle(GetAllProductPagedForRegisteredUsersQuery query, CancellationToken ct)
    {
        var result = await productService.GetAllProductPagedForRegisteredUsers(query.userId, new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            Take = query.PageSize,
            PageNumber = query.PageNumber
        }, ct);

        if (result.IsFailure)
            return Result.Failure<PagedResult<GetAllProductPagedQueryResponse>>(result.Errors);

        return Result.Success(PagedResult<GetAllProductPagedQueryResponse>.Create(GetAllProductPagedQueryResponse.Map(result.Value.Items)
            ,result.Value.TotalCount,result.Value.PageNumber,result.Value.PageSize));
    }
}
