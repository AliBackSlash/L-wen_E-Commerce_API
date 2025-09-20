namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveFromWishlist;

public record RemoveFromWishlistCommand(string userId,string productId) : ICommand;