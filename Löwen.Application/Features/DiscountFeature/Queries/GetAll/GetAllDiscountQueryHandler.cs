using Löwen.Application.Features.DiscountFeature.Queries.Response;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.DiscountFeature.Queries.GetAll;

internal class GetAllDiscountQueryHandler(IDiscountService discountService, IOptions<PaginationSettings> options) : ICommandHandler<GetAllDiscountQuery, PagedResult<DiscountResponse>>
{
    public async Task<Result<PagedResult<DiscountResponse>>> Handle(GetAllDiscountQuery query, CancellationToken ct)
    {
        var result = await discountService.GetAllPaged(new PaginationParams
        {
            maxPageSize = options.Value.maxPageSize,
            PageNumber = query.PageNumber,
            Take = query.PageSize
        }, ct);

        return Result.Success(PagedResult<DiscountResponse>.Create( DiscountResponse.map(result.Value.Items),result.Value.TotalCount,
            result.Value.PageNumber,result.Value.PageSize));
    }
}
