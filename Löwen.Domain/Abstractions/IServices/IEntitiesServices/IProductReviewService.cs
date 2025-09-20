namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductReviewService : IBasRepository<ProductReview,Guid>
{
    Task<bool> IsFound(Guid Id,CancellationToken ct);
}
