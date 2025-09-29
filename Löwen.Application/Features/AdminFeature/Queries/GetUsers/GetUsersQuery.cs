using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.AdminFeature.Queries.GetUsers;

public record GetUsersQuery(int PageNumber,byte PageSize) : ICommand<PagedResult<GetUsersQueryResponse>>;

