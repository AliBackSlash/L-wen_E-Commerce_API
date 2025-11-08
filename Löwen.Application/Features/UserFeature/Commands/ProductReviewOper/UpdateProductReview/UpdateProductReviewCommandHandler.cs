using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.UpdateProductReview;

internal class UpdateProductReviewCommandHandler(IProductReviewService productReviewService,IProductService productService) : ICommandHandler<UpdateProductReviewCommand>
{
    public async Task<Result> Handle(UpdateProductReviewCommand command, CancellationToken ct)
    {
        var ProductReview = await productReviewService.GetByIdAsync(Guid.Parse(command.productReviewId), ct);

        if (ProductReview is null)
            return Result.Failure(new Error("ProductReview.Update", $"ProductReview with Id {command.productReviewId} not found", ErrorType.Conflict));

        if (!await productService.IsFound(ProductReview.ProductId, ct))
            return Result.Failure(new Error("ProductReview.Update", $"Product with Id {ProductReview.ProductId} not found", ErrorType.Conflict));


        ProductReview.Rating = command.Rating ?? ProductReview.Rating;
        ProductReview.Review = command.Review ?? ProductReview.Review;

        var updateResult = await productReviewService.UpdateAsync(ProductReview,ct);


        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
        
    }
}
