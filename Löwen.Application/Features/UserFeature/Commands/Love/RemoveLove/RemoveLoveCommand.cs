using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.UserFeature.Commands.Love.RemoveLove;

public record RemoveLoveCommand(string userId,string productId) : ICommand;