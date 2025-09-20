namespace Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.UpdateProductReview;

public record UpdateProductReviewCommand(string productReviewId, char? Rating, string? Review) : ICommand;
