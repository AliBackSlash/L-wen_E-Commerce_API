using Löwen.Application.Common.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;

namespace Löwen.Infrastructure.Caching;

public class MemoryCacheService(IMemoryCache _cache) : ICacheService
{
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> _prefixTokens = new();
    public async Task<Result<TResult>?> GetOrCreateAsync<TResult>(
        string cacheKey,
        Func<Task<Result<TResult>>> action,
        int durationMinutes = 1,
        string? prefix = null)
    {
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(durationMinutes);

            if (!string.IsNullOrEmpty(prefix))
            {
                entry.AddExpirationToken(new CancellationChangeToken(
                    _prefixTokens.GetOrAdd(prefix, _ => new CancellationTokenSource()).Token));
            }

            return await action();
        });
    }

    public void RemoveByPrefix(string prefix)
    {
        if (_prefixTokens.TryRemove(prefix, out var cts))
        {
            cts.Cancel();
            cts.Dispose();
        }
    }
}