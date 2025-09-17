namespace Löwen.Application.Features.UserFeature.Commands.AddLove;

public record AddLoveCommand(string userId,string productId) : ICommand;