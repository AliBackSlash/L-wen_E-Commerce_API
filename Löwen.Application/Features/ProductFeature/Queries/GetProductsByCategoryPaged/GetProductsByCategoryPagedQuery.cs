namespace Löwen.Application.Features.ProductFeature.Queries.GetProductsByCategoryPaged;

public record GetProductsByCategoryPagedQuery(string Category, int PageNumber,byte PageSize) : IQuery<PagedResult<GetProductQueryResponse>>;
