using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.DiscountFeature.Commands.AssignDiscountToProduct;

internal class AssignDiscountToProductHandler(IDiscountService discountService) : ICommandHandler<AssignDiscountToProductCommand>
{
    public async Task<Result> Handle(AssignDiscountToProductCommand command, CancellationToken ct)
    {
        var result = await discountService.AssignDiscountToProduct(
            Guid.Parse(command.discountId),
            Guid.Parse(command.ProductId), ct);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
