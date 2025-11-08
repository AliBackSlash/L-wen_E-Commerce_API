
using Löwen.Application.Common.Caching;
using Löwen.Application.Messaging.Cache;

namespace Löwen.Application.Behaviors;

public class InvalidateCacheBehavior<TRequest, TResponse>(ICacheService _cache)
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IRequest<Result<TResponse>>
{
    public async Task<Result<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<Result<TResponse>> next,
        CancellationToken cancellationToken)
    {
        var result = await next();

        if (result.IsSuccess
            && request is IInvalidateCacheCommand invalidate)
            _cache.RemoveByPrefix(invalidate.Prefix);
        
        return result;
    }
}
