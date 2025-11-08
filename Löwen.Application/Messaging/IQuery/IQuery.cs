using Löwen.Application.Messaging.Cache;
using MediatR;


namespace Löwen.Application.Messaging.IQuery;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
public interface IQueryWithCache<TResponse> : IRequest<Result<TResponse>>, ICachableQuery;
