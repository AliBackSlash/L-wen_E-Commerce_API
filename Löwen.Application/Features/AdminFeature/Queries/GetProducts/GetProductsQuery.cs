using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProducts;

public record GetProductsQuery(int PageNumber,byte PageSize) : ICommand<PagedResult<GetProductsQueryResponse>>;

