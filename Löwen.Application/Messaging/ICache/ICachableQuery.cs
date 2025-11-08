namespace Löwen.Application.Messaging.Cache;

public interface ICachableQuery
{
    string CacheKey { get; }
    string Prefix { get; }
    int DurationMinutes { get; }
}
