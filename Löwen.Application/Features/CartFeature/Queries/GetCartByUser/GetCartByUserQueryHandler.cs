using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.Pagination;
using Microsoft.Extensions.Options;

namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

internal class GetCartByUserQueryHandler(ICartService cartService, IOptions<PaginationSettings> options)
    : IQueryHandler<GetCartByUserQuery, PagedResult<GetCartByUserQueryresponse>>
{
    public async Task<Result<PagedResult<GetCartByUserQueryresponse>>> Handle(GetCartByUserQuery command, CancellationToken ct)
    {
        var items = await  cartService.GetCartForUser(Guid.Parse(command.userId),
            new PaginationParams
            {
                maxPageSize = options.Value.maxPageSize,
                PageNumber = command.PageNumber,
                Take = command.PageSize,
            }, ct);

        return Result.Success(PagedResult<GetCartByUserQueryresponse>.Create(GetCartByUserQueryresponse.map(items.Value.Items)
            , items.Value.TotalPages
            , items.Value.PageNumber, items.Value.PageSize));
    }
}
