namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveProductReview;

public record RemoveProductReviewCommand(string productReviewId) : ICommand;