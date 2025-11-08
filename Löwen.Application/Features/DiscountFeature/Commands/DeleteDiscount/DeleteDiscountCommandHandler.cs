using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Application.Features.DiscountFeature.Commands.DeleteDiscount;

internal class DeleteDiscountCommandHandler(IDiscountService discountService) : ICommandHandler<RemoveDiscountCommand>
{
    public async Task<Result> Handle(RemoveDiscountCommand command, CancellationToken ct)
    {
        var discount = await discountService.GetByIdAsync(Guid.Parse(command.Id), ct);
        if (discount is null)
            return Result.Failure(Error.NotFound("Discount.NotFound", $"Discount with Id {command.Id} not found"));

        var deleteResult = await discountService.DeleteAsync(discount, ct);
        if (deleteResult.IsFailure)
            return Result.Failure(deleteResult.Errors);
        
        return Result.Success();
    }
}
