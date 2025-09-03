using Löwen.Domain.ErrorHandleClasses;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using Microsoft.Extensions.Primitives;

namespace Löwen.Presentation.API.CacheHelper;
public static class CacheHelper
{
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> _prefixTokens = new();

    public static readonly string Users_prefix = "User";
    public static readonly string admins_prefix = "admins";
    public static readonly string RootAdmin_prefix = "RootAdmin";
    public static readonly string OTP_prefix = "OTP";

    public static readonly int members_list_durationMinutes =  4;
    public static readonly int membership_list_durationMinutes = 2;
    public static readonly int admins_list_durationMinutes = 1;
    public static readonly int Individual_result_durationMinutes = 5;
    public static readonly int OTP_Code_durationMinutes = 5;

    /// <summary>
    /// Executes a MediatR request with caching and tracks keys under a prefix for invalidation.
    /// </summary>
    public static async Task<Result<TResult>?> GetOrCreateCachedAsync<TResult>(
        this IMemoryCache cache,
        string cacheKey,
        Func<Task<Result<TResult>>> mediatorCall,
        int durationMinutes = 1,
        string? prefix = null)
    {

        return await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(durationMinutes);

            if (!string.IsNullOrEmpty(prefix))
            {
                entry.AddExpirationToken(new CancellationChangeToken(_prefixTokens.GetOrAdd(prefix, _ => new CancellationTokenSource()).Token));
            }

            return await mediatorCall();
        });
    }

    /// <summary>
    /// Removes all cache entries stored under a specific prefix instantly.
    /// </summary>
    public static void RemoveByPrefix(this IMemoryCache cache, string prefix)
    {
        if (string.IsNullOrEmpty(prefix)) return;

        if (_prefixTokens.TryRemove(prefix, out var cts))
        {
            cts.Cancel();
            cts.Dispose();
        }

    }
}
