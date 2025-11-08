using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProducts;

public class GetProductsQueryHandler(IProductService productService,IOptions<PaginationSettings> PSettings ) : IQueryWithCacheHandler<GetProductsQuery, PagedResult<GetProductsQueryResponse>>
{
    public async Task<Result<PagedResult<GetProductsQueryResponse>>> Handle(GetProductsQuery query, CancellationToken ct)
    {
        var users = await productService.GetAllProductPagedToShowForAdmins(new PaginationParams
        {
            maxPageSize = PSettings.Value.maxPageSize,
            PageNumber = query.PageNumber,
            Take = query.PageSize
        },ct);
        if (users.IsFailure)
            return Result.Failure<PagedResult<GetProductsQueryResponse>> (users.Errors);

        return Result.Success(PagedResult<GetProductsQueryResponse>.Create(users.Value.Items.Select(x => new GetProductsQueryResponse
        {
            ProductId = x.ProductId,
            Name = x.Name,
            MainPrice = x.MainPrice,
            LoveCount = x.LoveCount,
            CreatedBy = x.CreatedBy,
            Status = x.Status

        }),users.Value.TotalCount,
            users.Value.PageNumber,users.Value.PageSize));
    }
}
