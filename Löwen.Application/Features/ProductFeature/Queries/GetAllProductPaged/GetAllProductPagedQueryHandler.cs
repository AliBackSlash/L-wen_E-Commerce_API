using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPaged;

public class GetAllProductPagedQueryHandler(IProductService productService,IOptions<PaginationSettings> options)
    : IQueryHandler<GetAllProductPagedQuery, PagedResult<GetProductQueryResponse>>
{
    public async Task<Result<PagedResult<GetProductQueryResponse>>> Handle(GetAllProductPagedQuery query, CancellationToken ct)
    {
        var result = await productService.GetProductsPaged(new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            Take = query.PageSize,
            PageNumber = query.PageNumber
        }, ct);

        if (result.IsFailure)
            return Result.Failure<PagedResult<GetProductQueryResponse>>(result.Errors);

        return Result.Success(PagedResult<GetProductQueryResponse>.Create(/*GetProductQueryResponse.Map(result.Value.Items)*/[]
            ,result.Value.TotalCount,result.Value.PageNumber,result.Value.PageSize));
    }
}
