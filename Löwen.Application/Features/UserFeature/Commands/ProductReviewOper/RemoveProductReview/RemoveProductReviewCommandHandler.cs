using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveProductReview;

internal class RemoveProductReviewCommandHandler(IProductReviewService productReviewService) : ICommandHandler<RemoveProductReviewCommand>
{
    public async Task<Result> Handle(RemoveProductReviewCommand command, CancellationToken ct)
    {
        var ProductReview = await productReviewService.GetByIdAsync(Guid.Parse(command.productReviewId), ct);

        if (ProductReview is null)
            return Result.Failure(new Error("ProductReview.Remove", $"ProductReview with Id {command.productReviewId} not found", ErrorType.Conflict));

        var RemoveResult = await productReviewService.DeleteAsync(ProductReview, ct);

        if (RemoveResult.IsFailure)
            return Result.Failure(RemoveResult.Errors);

        return Result.Success();
        
    }
}
