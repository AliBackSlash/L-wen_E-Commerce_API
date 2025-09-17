namespace Löwen.Application.Features.UserFeature.Commands.AddToWishlist;

public record AddLoveCommand(string userId,string productId) : ICommand;