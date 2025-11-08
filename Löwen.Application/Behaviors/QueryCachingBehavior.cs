using Löwen.Application.Common.Caching;
using Löwen.Application.Messaging.Cache;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Behaviors;

public class QueryCachingBehavior<TRequest, TResponse>(ICacheService _cache)
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IQueryWithCache<TResponse>
{

    public async Task<Result<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<Result<TResponse>> next,
        CancellationToken cancellationToken)
    {
        if (request is not ICachableQuery cq)
            return await next();

        var cached = await _cache.GetOrCreateAsync(
            cq.CacheKey,
            async () => await next(),
            cq.DurationMinutes,
            cq.Prefix
        );

        return cached ?? Result.Failure<TResponse>(new Error(
            "IQueryCachingBehavior",
            "Cache Error",
            ErrorType.Conflict
        ));
    }
}
