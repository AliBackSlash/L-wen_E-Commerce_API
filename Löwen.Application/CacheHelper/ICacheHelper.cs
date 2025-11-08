namespace Löwen.Application.Common.Caching;

public interface ICacheService
{
    Task<Result<TResult>?> GetOrCreateAsync<TResult>(
        string cacheKey,
        Func<Task<Result<TResult>>> action,
        int durationMinutes = 1,
        string? prefix = null);

    void RemoveByPrefix(string prefix);
}
