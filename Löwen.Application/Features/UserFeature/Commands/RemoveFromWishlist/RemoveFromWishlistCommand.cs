namespace Löwen.Application.Features.UserFeature.Commands.RemoveFromWishlist;

public record RemoveFromWishlistCommand(string userId,string productId) : ICommand;