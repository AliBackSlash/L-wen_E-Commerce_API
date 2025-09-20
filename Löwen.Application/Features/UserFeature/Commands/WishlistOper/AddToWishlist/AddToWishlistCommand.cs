namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.AddToWishlist;

public record AddToWishlistCommand(string userId,string productId) : ICommand;