using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.CartFeature.Queries.GetCartByUser;

internal class GetCartByUserQueryHandler(ICartService cartService) : IQueryHandler<GetCartByUserQuery, PagedResult<GetCartByUserQueryresponse>>
{
    public Task<Result<PagedResult<GetCartByUserQueryresponse>>> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
