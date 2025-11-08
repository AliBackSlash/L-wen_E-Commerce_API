using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveProductReview;

public record RemoveProductReviewCommand(string productReviewId) : ICommand;