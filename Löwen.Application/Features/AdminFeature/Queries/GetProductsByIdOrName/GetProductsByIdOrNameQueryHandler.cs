using Löwen.Application.Features.AdminFeature.Queries.GetProducts;
using Löwen.Application.Messaging.ICommand;
using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProductsByIdOrName;

public class GetProductsByIdOrNameQueryHandler(IProductService productService,IOptions<PaginationSettings> PSettings ) : IQueryWithCacheHandler<GetProductsByIdOrNameQuery, PagedResult<GetProductsQueryResponse>>
{
    public async Task<Result<PagedResult<GetProductsQueryResponse>>> Handle(GetProductsByIdOrNameQuery query, CancellationToken ct)
    {
        var users = await productService.GetAllProductPagedToShowForAdminsByIdOrName(query.IdOrName,new PaginationParams
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
