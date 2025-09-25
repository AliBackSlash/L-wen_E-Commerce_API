namespace Löwen.Application.Features.UserFeature.Commands.Love.AddOrder;

public record AddOrderCommand(string UserId) : ICommand<string>;
