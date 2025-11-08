using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.UserFeature.Commands.Love.AddLove;

public record AddLoveCommand(string userId,string productId) : ICommand;