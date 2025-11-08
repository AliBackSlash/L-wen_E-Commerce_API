using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductReviewsPaged;

public record GetAllProductReviewsPagedQuery(string productId, int PageNumber,byte PageSize) : IQuery<PagedResult<ProductReviewsResponse>>;
