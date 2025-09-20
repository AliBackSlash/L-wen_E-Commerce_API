using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.AddProductReview;

internal class AddProductReviewCommandHandler(IProductReviewService productReview,IProductService productService) : ICommandHandler<AddProductReviewCommand>
{
    public async Task<Result> Handle(AddProductReviewCommand command, CancellationToken ct)
    {

        Guid userId = Guid.Parse(command.userId);
        Guid productId = Guid.Parse(command.productId);

        
        if (!await productService.IsFound(productId, ct))
            return Result.Failure(new Error("ProductReview.Add", $"Product with Id {command.productId} not found", ErrorType.Conflict));

        var createResult = await productReview.AddAsync(new ProductReview
        {
            UserId = userId,
            ProductId = productId,
            Rating = command.Rating,
            Review = command.Review
        },ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
        
    }
}
