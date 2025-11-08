using MediatR;
namespace Löwen.Application.Messaging.IQuery;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;

public interface IQueryWithCacheHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQueryWithCache<TResponse>;
