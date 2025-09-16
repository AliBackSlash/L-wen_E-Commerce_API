using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.UserFeature.Queries.GetUsers;

public record GetUsersQuery(int PageNumber,byte PageSize) : ICommand<PagedResult<GetUsersQueryResponse>>;

