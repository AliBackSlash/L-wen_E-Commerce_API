using MediatR;


namespace Löwen.Application.Messaging.ICommand;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
