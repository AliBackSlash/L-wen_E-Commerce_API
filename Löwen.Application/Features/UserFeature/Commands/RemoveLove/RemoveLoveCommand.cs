namespace Löwen.Application.Features.UserFeature.Commands.RemoveLove;

public record RemoveLoveCommand(string userId,string productId) : ICommand;