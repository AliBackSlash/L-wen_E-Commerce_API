using Löwen.Application.Messaging.IQuery;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Entities.EntityForMapFunctionsResultOnly.Product;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.ProductFeature.Queries.GetProductsByCategoryPaged;

public class GetProductsByCategoryPagedQueryHandler(IProductService productService, IProductCategoryService categoryService, IOptions<PaginationSettings> options)
    : IQueryHandler<GetProductsByCategoryPagedQuery, PagedResult<GetProductQueryResponse>>
{
    public async Task<Result<PagedResult<GetProductQueryResponse>>> Handle(GetProductsByCategoryPagedQuery query, CancellationToken ct)
    {
        var Cat_Id = await categoryService.GetCategoryIdByCategoryName(query.Category, ct);
        if (Cat_Id == null)
            return Result.Failure<PagedResult<GetProductQueryResponse>>(new Error("IProductCategoryService.GetCategoryIdByCategoryName",
                $"Category {query.Category} not found", ErrorType.Conflict));

        var result = await productService.GetProductByCategoryAsync(Cat_Id.Value,new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            Take = query.PageSize,
            PageNumber = query.PageNumber
        }, ct);

        if (!result.Items.Any())
            return PagedResult<GetProductQueryResponse>.Create([], 0,  query.PageNumber,query.PageSize);


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
            ,result.TotalCount,result.PageNumber,result.PageSize));
    }
}
