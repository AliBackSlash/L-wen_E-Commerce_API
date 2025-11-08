using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.ProductFeature.Queries.GetProductById;

public record GetProductByIdQuery(string productId) : IQuery<GetProductByIdQueryResponse>;
