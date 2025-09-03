using MediatR;


namespace Löwen.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
