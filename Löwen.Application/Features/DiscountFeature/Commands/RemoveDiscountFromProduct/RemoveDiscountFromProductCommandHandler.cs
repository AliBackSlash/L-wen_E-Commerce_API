using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.DiscountFeature.Commands.RemoveDiscountFromProduct;

internal class RemoveDiscountFromProductCommandHandler(IDiscountService discountService) : ICommandHandler<RemoveDiscountFromProductCommand>
{
    public async Task<Result> Handle(RemoveDiscountFromProductCommand command, CancellationToken ct)
    {
        var result = await discountService.RemoveDiscountFromProduct(
            Guid.Parse(command.discountId),
            Guid.Parse(command.ProductId), ct);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
