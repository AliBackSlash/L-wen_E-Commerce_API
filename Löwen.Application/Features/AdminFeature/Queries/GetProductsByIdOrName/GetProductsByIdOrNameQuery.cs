using Löwen.Application.Features.AdminFeature.Queries.GetProducts;
using Löwen.Domain.Pagination;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProductsByIdOrName;

public record GetProductsByIdOrNameQuery(string IdOrName,int PageNumber,byte PageSize) : ICommand<PagedResult<GetProductsQueryResponse>>;

