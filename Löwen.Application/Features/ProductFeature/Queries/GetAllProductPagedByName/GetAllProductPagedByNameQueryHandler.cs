using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedByName;

public class GetAllProductPagedByNameQueryHandler(IProductService productService,IOptions<PaginationSettings> options)
    : IQueryHandler<GetAllProductPagedByNameQuery, PagedResult<GetProductQueryResponse>>
{
    public async Task<Result<PagedResult<GetProductQueryResponse>>> Handle(GetAllProductPagedByNameQuery query, CancellationToken ct)
    {
        var result = await productService.GetProductsPagedByName(query.Name,new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            Take = query.PageSize,
            PageNumber = query.PageNumber
        }, ct);

        
        return Result.Success(PagedResult<GetProductQueryResponse>.Create(result.Items.Select(x => new GetProductQueryResponse
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Description = x.Description,
            LoveCount = x.LoveCount,
            Price = x.Price,
            Discount = x.Discount,
            discountType = x.discountType,
            ProductImage = x.ProductImage,
            Status = x.Status,
            Rating = x.Rating,

        })
        , result.TotalCount,result.PageNumber,result.PageSize));
    }
}
