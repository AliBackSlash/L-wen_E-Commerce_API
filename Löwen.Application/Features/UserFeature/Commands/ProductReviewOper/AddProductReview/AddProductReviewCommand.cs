namespace Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.AddProductReview;

public record AddProductReviewCommand(string userId,string productId,  char Rating, string Review) : ICommand;
