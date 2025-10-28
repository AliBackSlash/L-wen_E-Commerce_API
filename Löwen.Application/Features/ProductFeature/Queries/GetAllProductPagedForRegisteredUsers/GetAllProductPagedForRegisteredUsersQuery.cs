namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedForRegisteredUsers;

public record GetAllProductPagedForRegisteredUsersQuery(Guid userId, int PageNumber,byte PageSize) : IQuery<PagedResult<GetProductQueryResponse>>;
