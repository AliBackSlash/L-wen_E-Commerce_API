using Löwen.Application.Features.DiscountFeature.Queries.Response;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Entities;
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

        return Result.Success(PagedResult<DiscountResponse>.Create(result.Items.Select(discount => new DiscountResponse
        {
            Id = discount.Id,
            Name = discount.Name,
            DiscountType = discount.DiscountType,
            DiscountValue = discount.DiscountValue,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate,
            IsActive = discount.IsActive,

        }),result.TotalCount,
            result.PageNumber,result.PageSize));
    }
}
